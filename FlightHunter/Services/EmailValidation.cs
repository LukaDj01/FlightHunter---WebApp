namespace FlightHunter.Services
{
    public static class EmailValidation
    {
        public static bool IsEmailValid(string email)
        {
            string[] allowedDomains = { "gmail.com", "hotmail.com", "@outlook.com"};
            string domain = email.Split('@').LastOrDefault();
            return allowedDomains.Contains(domain);
        }
    }
}