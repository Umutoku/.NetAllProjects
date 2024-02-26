namespace WebApp.Strategy.Models
{
    public class Settings
    {
        public static string claimDatabaseType = "DatabaseType"; // sayesinde claim'den database type alınabilir.
        public EDatabaseType DatabaseType { get; set; }

        public EDatabaseType GetDefaultDatabase => EDatabaseType.SqlServer;
    }
}
