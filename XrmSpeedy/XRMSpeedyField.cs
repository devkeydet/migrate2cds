using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace XrmSpeedy
{
    public class XRMSpeedyField : Microsoft.Xrm.Sdk.Metadata.MetadataBase
    {
        public string OriginalField { get; set; }
        public AttributeMetadata AttributeMetadata { get; set; }
        public bool Import { get; set; }

        public XRMSpeedyField(string orignalField, string type, string prefix,  bool import)
        {
            OriginalField = orignalField;
            Import = import;
            AttributeMetadata = SetMetadataType(type, prefix, orignalField);
        }

        public static AttributeMetadata SetMetadataType(string type, string prefix, string name)
        {
            AttributeMetadata value = new AttributeMetadata();
            switch (type)
            {
                case "Single Line of Text":
                    value = new StringAttributeMetadata();
                    ((StringAttributeMetadata)value).Format = Microsoft.Xrm.Sdk.Metadata.StringFormat.Text;
                    ((StringAttributeMetadata)value).MaxLength = 100;
                    break;
                case "Two Options":
                    value = new BooleanAttributeMetadata();
                    ((BooleanAttributeMetadata)value).OptionSet = new BooleanOptionSetMetadata(new OptionMetadata(
                        new Microsoft.Xrm.Sdk.Label("Yes", 1033), 10000),
                        new OptionMetadata(new Microsoft.Xrm.Sdk.Label("No", 1033), 10001));
                    ((BooleanAttributeMetadata)value).DefaultValue = false;
                    break;
                case "Whole Number":
                    value = new IntegerAttributeMetadata();
                    ((IntegerAttributeMetadata)value).Format = Microsoft.Xrm.Sdk.Metadata.IntegerFormat.None;
                    ((IntegerAttributeMetadata)value).MinValue = -2147483648;
                    ((IntegerAttributeMetadata)value).MaxValue = 2147483647;
                    break;
                case "Floating Point Number":
                    value = new DoubleAttributeMetadata();
                    ((DoubleAttributeMetadata)value).Precision = 2;
                    ((DoubleAttributeMetadata)value).MinValue = 0.00;
                    ((DoubleAttributeMetadata)value).MaxValue = 1000000000.00;
                    break;
                case "Decimal Number":
                    value = new DecimalAttributeMetadata();
                    ((DecimalAttributeMetadata)value).Precision = 2;
                    ((DecimalAttributeMetadata)value).MinValue = (decimal)0.00;
                    ((DecimalAttributeMetadata)value).MaxValue = (decimal)1000000000.00;
                    break;
                case "Currency":
                    value = new MoneyAttributeMetadata();
                    ((MoneyAttributeMetadata)value).Precision = 4;
                    ((MoneyAttributeMetadata)value).PrecisionSource = 2;
                    ((MoneyAttributeMetadata)value).MinValue = 0.0000;
                    ((MoneyAttributeMetadata)value).MaxValue = 1000000000.0000;
                    break;
                case "Multiple Lines of Text":
                    value = new MemoAttributeMetadata();
                    ((MemoAttributeMetadata)value).MaxLength = 2000;
                    break;
                case "Date and Time":
                    value = new DateTimeAttributeMetadata();
                    ((DateTimeAttributeMetadata)value).Format = Microsoft.Xrm.Sdk.Metadata.DateTimeFormat.DateOnly;
                    break;
            }
            value.SchemaName = prefix + "_" + name.ToLower();
            value.DisplayName = new Microsoft.Xrm.Sdk.Label(name, 1033);
            value.IsAuditEnabled = new BooleanManagedProperty(true);
            return value;
        }
    }
}
