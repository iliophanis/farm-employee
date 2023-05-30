namespace server.Modules.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetLocalDateTimeString(this DateTime? d)
        {
            return d?.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
        }

        public static string GetLocalDateString(this DateTime? d)
        {
            return d?.ToLocalTime().ToString("dd/MM/yyyy");
        }

        public static string GetLocalTimeString(this DateTime? d)
        {
            return d?.ToLocalTime().ToString("HH:mm");
        }

        public static string GetFullLocalDateTimeString(this DateTime? d)
        {
            return d?.ToLocalTime().ToString("dddd dd/MM/yyyy HH:mm");
        }

        public static string GetLocalDateStringView(this DateTime? d)
        {
            return d?.ToLocalTime().ToString("yyyy-MM-dd");
        }

        public static string GetLocalDateTimeString(this DateTime d)
        {
            return d.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
        }

        public static string GetLocalDateString(this DateTime d)
        {
            return d.ToLocalTime().ToString("dd/MM/yyyy");
        }

        public static string GetLocalTimeString(this DateTime d)
        {
            return d.ToLocalTime().ToString("HH:mm");
        }

        public static string GetFullLocalDateTimeString(this DateTime d)
        {
            return d.ToLocalTime().ToString("dddd dd/MM/yyyy HH:mm");
        }

        public static string GetLocalDateStringView(this DateTime d)
        {
            return d.ToLocalTime().ToString("yyyy-MM-dd");
        }

        public static string GetLocalDateStringView2(this DateTime d)
        {
            return d.ToLocalTime().ToString("dd-MM-yyyy");
        }
    }
}