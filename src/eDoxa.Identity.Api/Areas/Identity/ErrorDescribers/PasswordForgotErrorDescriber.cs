using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
