namespace ImbizoFoundation.Models
{
    public class MarketPlace
    {
        public string? companyName;
        public string? companyEmailAddress;
        public string? companyDescription;
        public string? companyLogoURL;

        public MarketPlace()
        {

        }

        public MarketPlace(string? companyName, string? companyEmailAddress, string? companyDescription, string? companyLogoURL)
        {
            this.companyName = companyName;
            this.companyEmailAddress = companyEmailAddress;
            this.companyDescription = companyDescription;
            this.companyLogoURL = companyLogoURL;
        }
    }
}
