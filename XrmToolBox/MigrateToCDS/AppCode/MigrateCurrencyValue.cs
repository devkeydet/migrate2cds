
namespace CDSTools
{
    public class MigrateCurrencyValue
    {
        public int Precision { get; set; }
        public int? PrecisionSource { get; set; }

        public MigrateCurrencyValue(int? precisionSource, int precision)
        {
            Precision = precision;
            PrecisionSource = precisionSource;
        }
    }
}
