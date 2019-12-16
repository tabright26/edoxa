namespace eDoxa.Clans.Api.Application.ErrorDescribers
{
    public class ClanErrorDescriber
    {
        public static string NameRequired()
        {
            return "Name is required";
        }

        public static string NameLength()
        {
            return "Name must be between 3 and 20 characters long";
        }

        public static string NameInvalid()
        {
            return "Name invalid. Only letters, numbers and hyphens allowed.";
        }

    }
}
