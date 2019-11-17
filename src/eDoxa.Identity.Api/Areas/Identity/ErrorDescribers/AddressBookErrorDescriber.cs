namespace eDoxa.Identity.Api.Areas.Identity.ErrorDescribers
{
    public class AddressBookErrorDescriber
    {
        public static string CountryRequired()
        {
            return "ErrorMessage";
        }

        public static string CountryInvalid()
        {
            return "Country invalid. Must be Canada or United States";
        }

        public static string Line1Required()
        {
            return "Main address is required";
        }

        public static string Line1Invalid()
        {
            return "Main address invalid. Must not have special characters";
        }

        public static string Line2Invalid()
        {
            return "Secondary address invalid. Must not have special characters";
        }

        public static string CityRequired()
        {
            return "City is required";
        }

        public static string CityInvalid()
        {
            return "City is invalid. Only letters, spaces and hyphens allowed";
        }

        public static string StateRequired()
        {
            return "State is required";
        }

        public static string StateInvalid()
        {
            return "State is invalid. Only letters, spaces and hyphens allowed";
        }

        public static string PostalCodeRequired()
        {
            return "Postal code is required";
        }

        public static string PostalCodeLength()
        {
            return "Postal code must be between 5 and 6 characters long";
        }

        public static string PostalCodeInvalidError()
        {
            return "Postal code is invalid. Only letters and numbers allowed";
        }
    }
}
