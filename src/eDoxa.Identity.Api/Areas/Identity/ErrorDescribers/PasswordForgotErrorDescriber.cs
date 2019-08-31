namespace eDoxa.Identity.Api.Areas.Identity.ErrorDescribers
{
    public class PasswordForgotErrorDescriber
    {
        public static string EmailRequired()
        {
            return "Email is required";
        }

        public static string EmailInvalid()
        {
            return "Email invalid. Only email with @ allowed";
        }

    }
}
