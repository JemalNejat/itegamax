namespace ADMIN.ITEGAMAX._4._0
{
    public class CLConnectionStrings
    {
        private static readonly CLConnectionStrings _instance;

        static CLConnectionStrings()
        {
            _instance = new CLConnectionStrings();
        }

        private CLConnectionStrings() { }

        public static CLConnectionStrings Instance
        {
            get { return _instance; }
        }
        public static string? MariaDbConnectionString { get; set; }

    }

    public class CLCustAppsettings
    {
        private static readonly CLCustAppsettings _instance;

        static CLCustAppsettings()
        {
            _instance = new CLCustAppsettings();
        }

        private CLCustAppsettings() { }

        public static CLCustAppsettings Instance
        {
            get { return _instance; }
        }
        public static int COMP_APP_ID { get; set; }
        public static string? COMP_APP_NAME { get; set; }
        public static string? COMP_APP_TITLE { get; set; }
        public static string? COMP_APP_TITLE2 { get; set; }
        public static string? COMP_APP_DESCRIPTION { get; set; }
        public static string? COMP_APP_SLOGAN { get; set; }
        public static string? COMP_APP_URL_ADMIN { get; set; }
        public static string? COMP_APP_URL_CDN { get; set; }
        public static string? COMP_APP_URL_PUBLISHER { get; set; }
        public static string? COMP_APP_URL_SERVICES { get; set; }
        public static string? COMP_SITE_URL { get; set; }
        public static string? POWERED_BY { get; set; }
        public static int COMP_APP_COREMENU_ID { get; set; }
        public static int COMP_STATUS_ACTIVE_ID { get; set; }
        public static bool ONDEVELOPING { get; set; }
        public static string XAPIKEY_IMAGES { get; set; }
        public static string IMAGE_SERVICES_PATH { get; set; }  

    }


    public class CLCompanySettings
    {
        private static readonly CLCompanySettings _instance;

        static CLCompanySettings()
        {
            _instance = new CLCompanySettings();
        }

        private CLCompanySettings() { }

        public static CLCompanySettings Instance
        {
            get { return _instance; }
        }

        public static string? COMPANY_NAME { get; set; }
        public static string? COMPANY_SHORT_NAME { get; set; }
        public static string? COMPANY_WEB { get; set; }
        public static string? COMPANY_POST_OFFICE { get; set; }
        public static string? COMPANY_POSTAL_CODE { get; set; }
        public static string? COMPANY_CITY { get; set; }
        public static string? COMPANY_LOCALITY { get; set; }
        public static string? COMPANY_MUNICIPALITY { get; set; }
        public static string? COMPANY_COUNTRY { get; set; }
        public static string? COMPANY_PHONE { get; set; }
    }
}
