namespace eDoxa.Identity.Api.Areas.Identity.ErrorDescribers
{
    public class InformationsErrorDescriber
    {
        public static string FirstNameRequired()
        {
            return "First name is required";
        }

        public static string FirstNameLength()
        {
            return "First name must be between 2 and 16 characters long";
        }

        public static string FirstNameInvalid()
        {
            return "First name invalid. Only letters and hyphens allowed";
        }

        public static string FirstNameUppercase()
        {
            return "First name invalid. Every part must start with an uppercase";
        }

        public static string LastNameRequired()
        {
            return "Last name is required";
        }

        public static string LastNameLength()
        {
            return "Last name must be between 2 and 16 characters long";
        }

        public static string LastNameInvalid()
        {
            return "Last name invalid. Only letters and hyphens allowed";
        }

        public static string LastNameUppercase()
        {
            return "Last name invalid. Every part must start with an uppercase";
        }

        public static string GenderRequired()
        {
            return "Gender is required";
        }

        public static string BirthDateRequired()
        {
            return "Birth date is required";
        }

        public static string BirthDateInvalid()
        {
            return "Birth date invalid";
        }
    }
}
