using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System.Formats.Asn1;

namespace StartFMS.Extensions.Configuration;
public static class Config
{
    /// <summary>
    /// 取得 ConnectionStrings 底下資料
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetConnectionString(string name)
    {
        IConfiguration config = GetConfiguration();
        return config.GetConnectionString(name);
    }

    public static IConfiguration GetAzureConfiguration(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException($"Error : {nameof(connectionString)} 不可為Null");
        }

        var builder = new ConfigurationBuilder();
        if (connectionString != null)
        {
            // 加入 Azure App Configuration 資料源
            builder.AddAzureAppConfiguration(options =>
            {
                options.Connect(connectionString)
                    // 如果您想要只加載指定的鍵，可以使用 Select 方法，例如：
                    //.Select(KeyFilter.AnyOf("MyApp:*"))
                    .ConfigureKeyVault(kv =>
                    {
                        kv.SetCredential(new DefaultAzureCredential());
                    });
            });
        }
        return builder.Build();
    }

    /// <summary>
    /// Get Local Environment / appsetting.json 
    /// </summary>
    /// <returns></returns>
    public static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
        return builder.Build();
    }

    /// <summary>
    /// Get Local Environment / appsetting.json 
    /// </summary>
    /// <param name="path">appsetting path</param>
    /// <returns></returns>
    public static IConfiguration GetConfiguration(string path)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
        return builder.Build();
    }

    /// <summary>
    /// Get Local Environment / appsetting.json 
    /// </summary>
    /// <returns></returns>
    public static IConfiguration GetConfiguration<T>()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(typeof(T).Assembly, optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
        return builder.Build();
    }

    /// <summary>
    /// Get Local Environment / appsetting.json 
    /// </summary>
    /// <param name="path">appsetting path</param>
    /// <returns></returns>
    public static IConfiguration GetConfiguration<T>(string path)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(typeof(T).Assembly, optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
        return builder.Build();
    }

    /// <summary>
    /// Get Local Environment / appsetting.json / UserSecrets
    /// </summary>
    /// <returns></returns>
    public static IConfiguration GetAzureConfiguration<T>()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(typeof(T).Assembly, optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
        return builder.Build();
    }

    /// <summary>
    ///  Get Local Environment / appsetting.json / UserSecrets
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">appsetting path</param>
    /// <returns></returns>
    public static IConfiguration GetAzureConfiguration<T>(string path)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(typeof(T).Assembly, optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
        return builder.Build();
    }
}
