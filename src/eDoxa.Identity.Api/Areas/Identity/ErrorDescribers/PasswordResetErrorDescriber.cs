using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDoxa.Identity.Api.Areas.Identity.ErrorDescribers
{
    public class PasswordResetErrorDescriber
    {
        public static string EmailRequired()
        {
            return "Email is required";
        }

        public static string EmailInvalid()
        {
            return "Email invalid. Only email with @ allowed";
        }

        public static string PasswordRequired()
        {
            return "Password is required";
        }

        public static string PasswordInvalid()
        {
            return "Password invalid. Must contain at least one number and one uppercase letter";
        }

        public static string PasswordLength()
        {
            return "Password invalid. Must be at least 8 characters long";
        }

        public static string PasswordSpecial()
        {
            return "Password invalid. Must contain a special character";
        }

    }
}
