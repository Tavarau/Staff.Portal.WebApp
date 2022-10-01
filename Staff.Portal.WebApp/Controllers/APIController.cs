namespace Staff.Portal.WebApp.Controllers;
public class APIController
{
    private static string _URL = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build())["API_URL"].ToString();   // ConfigurationManager.AppSettings["API_URL"];
    private static string _API_KEY_NAME = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build())["API_KEY_NAME"].ToString(); //ConfigurationManager.AppSettings["API_KEY_NAME"];
    private static string _API_KEY = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build())["API_KEY"].ToString(); // ConfigurationManager.AppSettings["API_KEY"];
    public static string URL
    {
        get { return _URL; }
    }
    public static string API_KEY_NAME
    {
        get { return _API_KEY_NAME; }
    }
    public static string API_KEY
    {
        get { return _API_KEY; }
    }
}
