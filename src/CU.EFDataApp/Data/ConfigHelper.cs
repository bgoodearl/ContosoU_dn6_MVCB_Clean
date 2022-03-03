namespace CU.EFDataApp.Data
{
    public static class ConfigHelper
    {
        internal static IConfigurationRoot GetConfiguration()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", true)
                        .AddJsonFile("appsettings.Development.json", true)
                        .AddJsonFile("appsettings.migrations.json", true)
                        .Build();

            return config;
        }
    }
}
