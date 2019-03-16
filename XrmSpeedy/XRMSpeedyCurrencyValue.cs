
namespace XrmSpeedy
{
    public class XRMSpeedyCurrencyValue
    {
        public int Precision { get; set; }
        public int? PrecisionSource { get; set; }

        public XRMSpeedyCurrencyValue(int? precisionSource, int precision)
        {
            Precision = precision;
            PrecisionSource = precisionSource;
        }
    }
}
