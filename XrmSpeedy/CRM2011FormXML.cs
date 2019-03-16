using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;

namespace XrmSpeedy
{
    public class CRM2011FormXML : XrmSpeedy.ICRM2011FormXML
    {
        /// <summary>
        /// Adds the new custom fields to the entity formXML
        /// </summary>
        /// <param name="service">CRM Organization service</param>
        /// <param name="entity">The string representation of the entity name</param>
        public void AddCustomFields(IOrganizationService service, string entity)
        {
            try
            {
                string formXML = GetFormXML(service, entity);

                if (!string.IsNullOrEmpty(formXML))
                {
                    string updatedFormXML = AddFieldsToForm(service, entity, formXML);
                    if (!string.IsNullOrEmpty(updatedFormXML))
                    {
                        UpdateForm(service, entity, updatedFormXML);
                    }
                }
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the custom fields from the form by re-loaded the unpublished formXML
        /// </summary>
        /// <param name="service">CRM Organization service</param>
        /// <param name="entity">The string representation of the entity name</param>
        /// <returns></returns>
        public bool RemoveCustomFields(IOrganizationService service, string entity)
        {
            try
            {
                string formXML = GetFormXML(service, entity);

                if (!string.IsNullOrEmpty(formXML))
                {
                    UpdateForm(service, entity, formXML);
                }

                return true;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        /// <summary>
        /// Applies the updated formXML
        /// </summary>
        /// <param name="service">CRM Organization service</param>
        /// <param name="entity">The string representation of the entity name</param>
        /// <param name="formXML">The entity's formXML</param>
        private void UpdateForm(IOrganizationService service, string entity, string formXML)
        {
            try
            {
                RetrieveEntityRequest entityRequest = new RetrieveEntityRequest
                {
                    LogicalName = entity,
                    EntityFilters = EntityFilters.Entity
                };

                RetrieveEntityResponse reResponse = (RetrieveEntityResponse)service.Execute(entityRequest);

                QueryExpression query = new QueryExpression
                {
                    EntityName = "systemform",
                    ColumnSet = new ColumnSet("formxml")
                };

                ConditionExpression expression1 = new ConditionExpression("type", ConditionOperator.Equal, 2);
                ConditionExpression expression2 = new ConditionExpression("objecttypecode", ConditionOperator.Equal,
                    reResponse.EntityMetadata.ObjectTypeCode.Value);

                query.Criteria.AddCondition(expression1);
                query.Criteria.AddCondition(expression2);

                RetrieveMultipleRequest rmRequest = new RetrieveMultipleRequest();
                rmRequest.Query = query;

                var rmResponse = (RetrieveMultipleResponse)service.Execute(rmRequest);

                var entityResponse = rmResponse.EntityCollection.Entities.FirstOrDefault();
                entityResponse.Attributes["formxml"] = formXML;

                service.Update(entityResponse);
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the formXML of the given entity
        /// </summary>
        /// <param name="service">CRM Organization service</param>
        /// <param name="entity">The string representation of the entity name</param>
        /// <returns>The entity's formXML</returns>
        private string GetFormXML(IOrganizationService service, string entity)
        {
            try
            {
                RetrieveEntityRequest entityRequest = new RetrieveEntityRequest
                {
                    LogicalName = entity,
                    EntityFilters = EntityFilters.Entity,
                    RetrieveAsIfPublished = false,
                };

                RetrieveEntityResponse entityResponse = (RetrieveEntityResponse)service.Execute(entityRequest);

                QueryExpression query = new QueryExpression
                {
                    EntityName = "systemform",
                    ColumnSet = new ColumnSet("formxml")
                };

                ConditionExpression expression1 = new ConditionExpression("type", ConditionOperator.Equal, 2);
                ConditionExpression expression2 = new ConditionExpression("objecttypecode", ConditionOperator.Equal, entityResponse.EntityMetadata.ObjectTypeCode.Value);

                query.Criteria.AddCondition(expression1);
                query.Criteria.AddCondition(expression2);

                RetrieveMultipleRequest rmRequest = new RetrieveMultipleRequest();
                rmRequest.Query = query;

                var rmResponse = (RetrieveMultipleResponse)service.Execute(rmRequest);
                return rmResponse.EntityCollection.Entities.FirstOrDefault().Attributes["formxml"].ToString();
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds the new fields to the formXML
        /// </summary>
        /// <param name="service">CRM Organization service</param>
        /// <param name="entity">The string representation of the entity name</param>
        /// <param name="formXML">The entity's formXML</param>
        /// <returns>The formXML updated with the new fields</returns>
        private string AddFieldsToForm(IOrganizationService service, string entity, string formXML)
        {
            XElement form = XElement.Parse(formXML);

            IEnumerable<XElement> rows = from elem in form.Descendants("tabs")
                                             .Descendants("tab").Where(e => e.Attribute("IsUserDefined").Value.Equals("1"))
                                             .Descendants("columns")
                                             .Descendants("column")
                                             .Descendants("sections")
                                             .Descendants("section")
                                             .Descendants("rows")
                                         select elem;

            IOrderedEnumerable<AttributeMetadata> fields = GetAttributeMetaData(service, entity);

            //Add fields based on the standard 2 column layout
            int fieldCount = 0;
            XElement row = new XElement("row");
            foreach (AttributeMetadata field in fields)
            {
                if (fieldCount % 2 == 0)
                    row = new XElement("row");

                XElement cell = new XElement("cell");
                cell.Add(new XAttribute("id", "{" + Guid.NewGuid().ToString() + "}"));
                XElement labels = new XElement("labels");
                XElement label = new XElement("label");
                label.Add(new XAttribute("description", field.DisplayName.LocalizedLabels.Where(l => l.LanguageCode == 1033).FirstOrDefault().Label));
                label.Add(new XAttribute("languagecode", "1033"));
                labels.Add(label);
                cell.Add(labels);
                XElement control = new XElement("control");
                control.Add(new XAttribute("id", field.SchemaName));
                control.Add(new XAttribute("classid", "{" + GetControlClassId(field) + "}"));
                control.Add(new XAttribute("datafieldname", field.SchemaName));
                control.Add(new XAttribute("addedby", "XrmSpeedy"));
                cell.Add(control);
                row.Add(cell);

                fieldCount++;

                if (fieldCount % 2 == 0 || fieldCount == fields.Count())
                    rows.FirstOrDefault().Add(row);
            }


            return form.ToString();
        }

        /// <summary>
        /// Retrieves the list of all custom fields that are not on the form by default and are able to be added to the form
        /// </summary>
        /// <param name="service">CRM Organization service</param>
        /// <param name="entity">The string representation of the entity name</param>
        /// <returns>List of AttributeMetaData (fields)</returns>
        private IOrderedEnumerable<AttributeMetadata> GetAttributeMetaData(IOrganizationService service, string entity)
        {
            try
            {
                RetrieveEntityRequest entityRequest = new RetrieveEntityRequest
                {
                    LogicalName = entity,
                    EntityFilters = EntityFilters.Attributes,
                    RetrieveAsIfPublished = true

                };

                RetrieveEntityResponse entityResponse = (RetrieveEntityResponse)service.Execute(entityRequest);
                IOrderedEnumerable<AttributeMetadata> fields = entityResponse.EntityMetadata.Attributes
                                                                .Where(a => a.IsCustomAttribute == true)
                                                                .Where(a => a.IsPrimaryId == false)
                                                                .Where(a => a.IsPrimaryName == false)
                                                                .Where(a => a.IsValidForUpdate == true)
                                                                .OrderBy(a => a.LogicalName);
                return fields;
            }
            catch (FaultException<OrganizationServiceFault>)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines the classid of a control based on the AttributeMetadata.AttributeType & the 
        /// specific type's AttributeMetadata subclass
        /// http://msdn.microsoft.com/en-us/library/gg334472.aspx
        /// </summary>
        /// <param name="meta"></param>
        /// <returns>The string representation </returns>
        public string GetControlClassId(AttributeMetadata meta)
        {
            switch (meta.AttributeType.ToString())
            {
                case "Boolean":
                    return "67FAC785-CD58-4f9f-ABB3-4B7DDC6ED5ED";
                case "Money":
                    return "533B9E00-756B-4312-95A0-DC888637AC78";
                case "DateTime":
                    return "5B773807-9FB2-42db-97C3-7A91EFF8ADFF";
                case "Decimal":
                    return "C3EFE0C3-0EC6-42be-8349-CBD9079DFD8E";
                case "Double":
                    return "0D2C745A-E5A8-4c8f-BA63-C6D3BB604660";
                case "Integer":
                    return "C6D124CA-7EDA-4a60-AEA9-7FB8D318B68F";
                case "Memo":
                    return "E0DECE4B-6FC8-4a8f-A065-082708572369";
                case "Picklist":
                    return "3EF39988-22BB-4f0b-BBBE-64B5A3748AEE";
                case "Lookup":
                    return "270BD3DB-D9AF-4782-9025-509E298DEC0A";
                case "String":
                    StringAttributeMetadata s = (StringAttributeMetadata)meta;
                    string stringVal = string.Empty;
                    switch (s.Format.ToString())
                    {
                        case "Email":
                            stringVal = "ADA2203E-B4CD-49be-9DDF-234642B43B52";
                            break;
                        case "Text":
                            stringVal = "4273EDBD-AC1D-40d3-9FB2-095C621B552D";
                            break;
                        case "TextArea":
                            stringVal = "E0DECE4B-6FC8-4a8f-A065-082708572369";
                            break;
                        case "TickerSymbol":
                            stringVal = "1E1FC551-F7A8-43af-AC34-A8DC35C7B6D4";
                            break;
                        case "Url":
                            stringVal = "71716B6C-711E-476c-8AB8-5D11542BFB47";
                            break;
                    }
                    return stringVal;
                default:
                    return string.Empty;
            }
        }
    }
}
