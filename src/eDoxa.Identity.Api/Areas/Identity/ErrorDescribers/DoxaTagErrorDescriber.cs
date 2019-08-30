using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDoxa.Identity.Api.Areas.Identity.ErrorDescribers
{
    public class DoxaTagErrorDescriber
    {
        public static string Required()
        {
            return "DoxaTag is required";
        }

        public static string Length()
        {
            return "DoxaTag must be between 2 and 16 characters long";
        }

        public static string Invalid()
        {
            return "DoxaTag invalid. May only contains (a-z,A-Z,_)";
        }

        public static string InvalidUnderscore()
        {
            return "DoxaTag invalid. Cannot start or end with _";
        }
    }
}
