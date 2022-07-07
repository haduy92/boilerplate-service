namespace BoilerplateService.Infrastructures.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToString(this DateTime? dateTime, string format)
        {
            if (dateTime != null && dateTime.HasValue)
            {
                return dateTime.Value.ToString(format);
            }

            return string.Empty;
        }

        public static bool Between(this DateTime dateTime, DateTime fromTime, DateTime toTime)
        {
            return dateTime >= fromTime && dateTime <= toTime;
        }

        public static bool Between(this DateTime? dateTime, DateTime? fromTime, DateTime? toTime)
        {
            if (!dateTime.HasValue)
            {
                return false;
            }

            var from = fromTime ?? DateTime.MinValue;
            var to = toTime ?? DateTime.MaxValue;

            return dateTime >= from && dateTime <= to;
        }
    }
}