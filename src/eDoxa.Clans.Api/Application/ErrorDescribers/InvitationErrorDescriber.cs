namespace eDoxa.Clans.Api.Application.ErrorDescribers
{
    public class InvitationErrorDescriber
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
