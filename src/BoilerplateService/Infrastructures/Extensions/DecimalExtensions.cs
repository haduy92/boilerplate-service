namespace BoilerplateService.Infrastructures.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal Round(this decimal d, int decimals = 2)
        {
            return decimal.Round(d, decimals);
        }

        public static decimal Round(this decimal? d, int decimals = 2)
        {
            return decimal.Round(d ?? 0, decimals);
        }
    }
}