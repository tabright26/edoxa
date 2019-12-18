namespace eDoxa.Clans.Api.Application.ErrorDescribers
{
    public class CandidatureErrorDescriber
    {
        public static string UserIdRequired()
        {
            return "UserId is required";
        }

        public static string ClanIdRequired()
        {
            return "ClanId is required";
        }

    }
}
