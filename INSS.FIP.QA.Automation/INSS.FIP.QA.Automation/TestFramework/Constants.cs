

namespace INSS.FIP.QA.Automation.TestFramework;

public static class Constants
{    
    public static string StartPageUrl => WebDriverFactory.Config["BaseUrl"];
    public static string Browser => WebDriverFactory.Config["Browser"];
    public static string AdminUserName => WebDriverFactory.Config["AdminUsername"];
    public static string AdminPassword => WebDriverFactory.Config["AdminPassword"];
    //Insolvency Practitioner Details used for creating DB record and verification on IP Details page
    public static string title = "Mr";
    public static string firstName = "TesterFirstName";
    public static string surname = "Testsurname";
    public static string telephone = "0987654321";
    public static string fax = "1234567890";
    public static string IPNumber = "9999";
    public static string authorisingBody = "ICAEW";
    public static string email = "testemail@test.co.uk";
    public static string companyName = "Midland Electrical Engineering";
    public static string postcode = "B43 6JN";
    public static string town = "MadeUpTown";
}