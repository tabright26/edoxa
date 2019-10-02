namespace eDoxa.Organizations.Clans.Api.Areas.Clans.ErrorDescribers
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
