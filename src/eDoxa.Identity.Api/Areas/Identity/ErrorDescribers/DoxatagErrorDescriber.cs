namespace eDoxa.Identity.Api.Areas.Identity.ErrorDescribers
{
    public static class DoxatagErrorDescriber
    {
        public static string Required()
        {
            return "Doxatag is required";
        }

        public static string Length()
        {
            return "Doxatag must be between 2 and 16 characters long";
        }

        public static string Invalid()
        {
            return "Doxatag invalid. May only contains (a-z,A-Z,_)";
        }

        public static string InvalidUnderscore()
        {
            return "Doxatag invalid. Cannot start or end with _";
        }
    }
}
