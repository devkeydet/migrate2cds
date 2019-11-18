using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.ComponentModel;

namespace CDSTools
{
    public enum AttributeMetadataType
    {
        [Description("Single Line of Text")]
        SingleLineOfText = 1,
        [Description("Two Options")]
        TwoOptions,
        [Description("Whole Number")]
        WholeNumber,
        [Description("Floating Point Number")]
        FloatingPointNumber,
        [Description("Decimal Number")]
        DecimalNumber,
        [Description("Currency")]
        Currency,
        [Description("Multiple Lines of Text")]
        MultipleLinesOfText,
        [Description("Date and Time")]
        DateAndTime
    }

    public class MigrateField: MigrateItemBase //: MetadataBase //, INotifyPropertyChanged
    {
        // public event PropertyChangedEventHandler PropertyChanged;
        string _prefix = MigrateDataBase.PREFIX;
        string _originalField = "new field";
        AttributeMetadataType _type = AttributeMetadataType.SingleLineOfText;

        [Category("Field Properties")]
        public int LanguageCode { get; set; } = 1033;

        [Category("Field Properties")]
        public string OriginalField {
            get=> _originalField;
            set {
                _originalField = value;

                UpdateFields();

                SetMetadataType();
            }
        }

        [Category("Field Properties")]
        public override bool Import { get; set; } = true;

        [Category("Field Properties")]
        public string DisplayName { get; set; }

        [Category("Field Properties")]
        public string Prefix {
            get =>_prefix;
            set {
                _prefix = value;

                UpdateFields();
            }
        }

        [Category("Field Properties")]
        public string SchemaName { get; set; }

        [Category("Field Properties")]
        public AttributeMetadataType AttributeType
        {
            get => _type;
            set {
                _type = value;
                SetMetadataType();
            }
        }

        [DisplayName("Attribute Type Properties")]
        [Description("Additional properties related to the selected AttributeMetadata Type")]
        [Category("Metadata Properties")]
        public IAttribType TypeProperties { get; set; }

        /// <summary>
        /// Default constructor for designer create 
        /// </summary>
        public MigrateField()
        {
            SchemaName = _prefix + "_" + OriginalField.ToLower().Replace(" ", "");

            SetMetadataType();
        }

        /// <summary>
        /// General constructor for reading DB
        /// </summary>
        /// <param name="orignalField"></param>
        /// <param name="type"></param>
        /// <param name="prefix"></param>
        /// <param name="import"></param>
        public MigrateField(string orignalField, AttributeMetadataType attrType, string prefix, bool import)
        {
            Import = import;

            _prefix = prefix;
            _type = attrType;

            OriginalField = orignalField;
            DisplayName = orignalField;

            UpdateFields();

            SetMetadataType();
        }

        private void UpdateFields() {
            SchemaName = _prefix + "_" + OriginalField.ToLower()
                                                      .Replace(" ", "")
                                                      .Replace("-", "_")
                                                      .Replace("/", "_");
        }

        /// <summary>
        /// Based on the attribute metadata type, preset some of the details
        /// </summary>
        public void SetMetadataType()
        {
            switch (AttributeType)
            {
                case AttributeMetadataType.SingleLineOfText:
                    TypeProperties = new StringAttrib()
                    {
                        Format = StringFormat.Text,
                        MaxLength = 100
                    };
                    break;
                case AttributeMetadataType.TwoOptions:
                    TypeProperties = new BooldeanAttrib()
                    {
                        LanguageCode = LanguageCode,
                        TrueOption = new BooldeanAttrib.Option() { Label = "Yes", Value = 10000 },
                        FalseOption = new BooldeanAttrib.Option() { Label = "No", Value = 10001 },
                        DefaultValue = false
                    };
                    break;
                case AttributeMetadataType.WholeNumber:
                    TypeProperties = new IntegerAttrib()
                    {
                        Format = IntegerFormat.None,
                        MinValue = -2147483648,
                        MaxValue = 2147483647
                    };
                    break;
                case AttributeMetadataType.FloatingPointNumber:
                    TypeProperties = new DoubleAttrib()
                    {
                        Precision = 2,
                        MinValue = 0.00,
                        MaxValue = 1000000000.00
                    };
                    break;
                case AttributeMetadataType.DecimalNumber:
                    TypeProperties = new DecimalAttrib()
                    {
                        Precision = 2,
                        MinValue = (decimal)0.00,
                        MaxValue = (decimal)1000000000.00
                    };
                    break;
                case AttributeMetadataType.Currency:
                    TypeProperties = new MoneyAttrib()
                    {
                        Precision = 4,
                        PrecisionSource = 2,
                        MinValue = 0.0000,
                        MaxValue = 1000000000.0000
                    };
                    break;
                case AttributeMetadataType.MultipleLinesOfText:
                    TypeProperties= new MemoAttrib()
                    {
                        MaxLength = 2000
                    };
                    break;
                case AttributeMetadataType.DateAndTime:
                    TypeProperties = new DateAndTimeAttrib()
                    {
                        Format = DateTimeFormat.DateOnly
                    };
                    break;
            }
        }

        /// <summary>
        /// Get the Attribute Metadata for this Attribute 
        /// </summary>
        /// <returns></returns>
        public AttributeMetadata ToMetadata() {
            // AttributeMetadata _metadata;
            var meta = this.TypeProperties.ToMetadata();

            meta.SchemaName = SchemaName;
            meta.DisplayName = new Label(OriginalField, LanguageCode);
            meta.IsAuditEnabled = new BooleanManagedProperty(true);

            return meta;
        }

        public override string ToString()
        {
            return $"{DisplayName} ({SchemaName})";
        }
    }

    #region helper classes for attribute type data - subset of Attribute Metadata so we can present for edits.

    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public interface IAttribType {
        AttributeMetadata ToMetadata();
    }

    /// <summary>
    /// StringAttributeMetadata
    /// </summary>
    public class StringAttrib : IAttribType {

        public int? MaxLength { get; set; }
        public StringFormat? Format { get; set; }

        public AttributeMetadata ToMetadata()
        {
            return new StringAttributeMetadata()
            {
                Format = Format,
                MaxLength = MaxLength
            };
        }
        public override string ToString()
        {
            return $"Length: {MaxLength.ToString()}, Format: {Format.ToString()}";
        }
    }

    /// <summary>
    /// MoneyAttributeMetadata
    /// </summary>
    public class MoneyAttrib : IAttribType {

        public double? MaxValue { get; set; }
        public double? MinValue { get; set; }
        public int? Precision { get; set; }
        public int? PrecisionSource { get; set; }

        public AttributeMetadata ToMetadata()
        {
            return new MoneyAttributeMetadata()
            {
                Precision = Precision,
                PrecisionSource = PrecisionSource,
                MinValue = MinValue,
                MaxValue = MaxValue
            };
        }
        public override string ToString()
        {
            return $"({MinValue.ToString()}, {MaxValue.ToString()}), Precision: {Precision?.ToString()}";
        }
    }

    /// <summary>
    /// DateTimeAttributeMetadata
    /// </summary>
    public class DateAndTimeAttrib : IAttribType
    {
        public DateTimeFormat? Format { get; set; }

        public AttributeMetadata ToMetadata()
        {
            return new DateTimeAttributeMetadata()
            {
                Format = Format
            };
        }
        public override string ToString()
        {
            return $"Format: {Format.ToString()}";
        }
    }

    /// <summary>
    /// DecimalAttributeMetadata
    /// </summary>
    public class DecimalAttrib : IAttribType
    {
        public decimal? MaxValue { get; set; }
        public decimal? MinValue { get; set; }
        public int? Precision { get; set; }

        public AttributeMetadata ToMetadata()
        {
            return new DecimalAttributeMetadata()
            {
                Precision = Precision,
                MinValue = MinValue,
                MaxValue = MaxValue
            };
        }

        public override string ToString()
        {
            return $"({MinValue.ToString()}, {MaxValue.ToString()}), Precision: {Precision?.ToString()}"; 
        }
    }

    /// <summary>
    /// MemoAttributeMetadata
    /// </summary>
    public class MemoAttrib : IAttribType
    {
        public int? MaxLength { get; set; }
        public StringFormat? Format { get; set; }

        public AttributeMetadata ToMetadata()
        {        
            return new MemoAttributeMetadata()
            {
                MaxLength = MaxLength
            };
        }
        public override string ToString()
        {
            return $"Length: {MaxLength.ToString()}";
        }
    }

    /// <summary>
    /// DoubleAttributeMetadata
    /// </summary>
    public class DoubleAttrib : IAttribType
    {
        public double? MaxValue { get; set; }
        public double? MinValue { get; set; }
        public int? Precision { get; set; }
        public AttributeMetadata ToMetadata()
        {
            return new DoubleAttributeMetadata()
            {
                Precision = Precision,
                MinValue = MinValue,
                MaxValue = MaxValue
            };
        }
        public override string ToString()
        {
            return $"({MinValue.ToString()}, {MaxValue.ToString()}), Precision: {Precision?.ToString()}";
        }
    }

    /// <summary>
    /// IntegerAttributeMetadata
    /// </summary>
    public class IntegerAttrib : IAttribType
    {
        public IntegerFormat? Format { get; set; }
        public int? MaxValue { get; set; }
        public int? MinValue { get; set; }

        public AttributeMetadata ToMetadata()
        {
            return new IntegerAttributeMetadata()
            {
                Format = Format,
                MinValue = MinValue,
                MaxValue = MaxValue
            };
        }
        public override string ToString()
        {
            return $"({MinValue.ToString()}, {MaxValue.ToString()}), Format: {Format?.ToString()}";
        }

    }

    /// <summary>
    /// BooleanAttributeMetadata
    /// </summary>
    public class BooldeanAttrib : IAttribType
    {
        public Option TrueOption { get; set; }
        public Option FalseOption { get; set; }
        public bool DefaultValue { get; set; }
        public int LanguageCode { get; set; } = 1033;

        public AttributeMetadata ToMetadata()
        {
            return new BooleanAttributeMetadata()
            {
                OptionSet = new BooleanOptionSetMetadata(
                    TrueOption.ToMetadata(LanguageCode),
                    FalseOption.ToMetadata(LanguageCode)),
                    DefaultValue = DefaultValue
            };
        }

        public override string ToString()
        {
            return $"True: {TrueOption.ToString()}, False: {FalseOption.ToString()}";
        }

        [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
        public class Option {
            public string Label { get; set; }
            public int Value { get; set; }
            internal OptionMetadata ToMetadata(int languageCode)
            {
                return new OptionMetadata(new Label(Label, languageCode), Value);
            }

            public override string ToString()
            {
                return $"{Label} ({Value.ToString()})";
            }
        }

    }
    #endregion
}
