import React from "react";

const Account = React.lazy(() => import("./views/User/Account/Account"));
const AccountCashier = React.lazy(() => import("./views/User/Profile/PaymentMethods/PaymentMethods"));
const AccountTransactionHistory = React.lazy(() => import("./views/User/Account/TransactionHistory/TransactionHistory"));
const Arena = React.lazy(() => import("./views/Arena/Arena"));
const ArenaChallenges = React.lazy(() => import("./views/Arena/Challenges/Challenges"));
const ArenaChallengeDetails = React.lazy(() => import("./views/Arena/Challenges/Details/Details"));
const ArenaChallengeHistory = React.lazy(() => import("./views/Arena/Challenges/History/History"));
const ArenaGames = React.lazy(() => import("./views/Arena/Games/Games"));
const ArenaTournaments = React.lazy(() => import("./views/Arena/Tournaments/Tournaments"));
const ArenaTournamentDetails = React.lazy(() => import("./views/Arena/Tournaments/Details/Details"));
const ArenaTournamentHistory = React.lazy(() => import("./views/Arena/Tournaments/History/History"));
const Marketplace = React.lazy(() => import("./views/Marketplace/Marketplace"));
const NewsFeeds = React.lazy(() => import("./views/NewsFeeds/NewsFeeds"));
const Profile = React.lazy(() => import("./views/User/Profile"));
const Structures = React.lazy(() => import("./Structures"));
const StructureClans = React.lazy(() => import("./views/Organizations/Clans/Clans"));
const StructureClanDashboard = React.lazy(() => import("./views/Organizations/Clans/Dashboard/Dashboard"));
const StructureClanDetails = React.lazy(() => import("./views/Organizations/Clans/Details/Details"));
const StructureClanMarketplace = React.lazy(() => import("./views/Organizations/Clans/Marketplace/Marketplace"));
const StructureLeagues = React.lazy(() => import("./views/Organizations/Leagues/Leagues"));
const StructureLeagueDashboard = React.lazy(() => import("./views/Organizations/Leagues/Dashboard/Dashboard"));
const StructureLeagueDetails = React.lazy(() => import("./views/Organizations/Leagues/Details/Details"));
const StructureLeagueMarketplace = React.lazy(() => import("./views/Organizations/Leagues/Makertplace/Marketplace"));
const StructureTeams = React.lazy(() => import("./views/Organizations/Teams/Teams"));
const StructureTeamDashboard = React.lazy(() => import("./views/Organizations/Teams/Dashboard/Dashboard"));
const StructureTeamDetails = React.lazy(() => import("./views/Organizations/Teams/Details/Details"));
const StructureTeamMarketplace = React.lazy(() => import("./views/Organizations/Teams/Makertplace/Marketplace"));

// https://github.com/ReactTraining/react-router/tree/master/packages/react-router-config
const routes = [
  { path: "/", name: "Home", exact: true, secure: false, scopes: [] },
  { path: "/account/overview", name: "Account Overview", component: Account, exact: true, secure: true, scopes: [] },
  { path: "/account/payment-methods", name: "Payment Methods", component: AccountCashier, exact: true, secure: true, scopes: [] },
  { path: "/account/transaction-history", name: "Transaction History", component: AccountTransactionHistory, exact: true, secure: true, scopes: [] },
  { path: "/arena", name: "Arena", component: Arena, exact: true, secure: false, scopes: [] },
  { path: "/arena/challenges", name: "Challenges", component: ArenaChallenges, exact: true, secure: true, scopes: [] },
  { path: "/arena/challenges/:challengeId", name: "Challenge Details", component: ArenaChallengeDetails, exact: true, secure: true, scopes: [] },
  { path: "/arena/challenge-history", name: "Challenge History", component: ArenaChallengeHistory, exact: true, secure: true, scopes: [] },
  { path: "/arena/games", name: "Games", component: ArenaGames, exact: true, secure: true, scopes: [] },
  { path: "/arena/tournaments", name: "Tournaments", component: ArenaTournaments, exact: true, secure: true, scopes: [] },
  { path: "/arena/tournaments/:tournamentId", name: "Tournament Details", component: ArenaTournamentDetails, exact: true, secure: true, scopes: [] },
  { path: "/arena/tournament-history", name: "Tournament History", component: ArenaTournamentHistory, exact: true, secure: true, scopes: [] },
  { path: "/marketplace", name: "Marketplace", component: Marketplace, exact: true, secure: false, scopes: [] },
  { path: "/news-feeds", name: "News Feeds", component: NewsFeeds, exact: true, secure: false, scopes: [] },
  { path: "/profile", name: "Profile Overview", component: Profile, exact: false, secure: true, scopes: [] },
  { path: "/structures", name: "Structures", component: Structures, exact: true, secure: false, scopes: [] },
  { path: "/structures/clans", name: "Clans", component: StructureClans, exact: true, secure: false, scopes: [] },
  { path: "/structures/clans/:clanId", name: "Clan Details", component: StructureClanDetails, exact: true, secure: true, scopes: [] },
  { path: "/structures/clans/:clanId/dashboard", name: "Clan Dashboard", component: StructureClanDashboard, exact: true, secure: true, scopes: [] },
  { path: "/structures/clans/:clanId/marketplace", name: "Clan Marketplace", component: StructureClanMarketplace, exact: true, secure: true, scopes: [] },
  { path: "/structures/leagues", name: "Leagues", component: StructureLeagues, exact: true, secure: false, scopes: [] },
  { path: "/structures/leagues/:leagueId", name: "League Details", component: StructureLeagueDetails, exact: true, secure: true, scopes: [] },
  { path: "/structures/leagues/:leagueId/dashboard", name: "League Dashboard", component: StructureLeagueDashboard, exact: true, secure: true, scopes: [] },
  { path: "/structures/leagues/:leagueId/marketplace", name: "League Marketplace", component: StructureLeagueMarketplace, exact: true, secure: true, scopes: [] },
  { path: "/structures/teams", name: "Teams", component: StructureTeams, exact: true, secure: false, scopes: [] },
  { path: "/structures/teams/:teamId", name: "Team Details", component: StructureTeamDetails, exact: true, secure: true, scopes: [] },
  { path: "/structures/teams/:teamId/dashboard", name: "Team Dashboard", component: StructureTeamDashboard, exact: true, secure: true, scopes: [] },
  { path: "/structures/teams/:teamId/marketplace", name: "Team Marketplace", component: StructureTeamMarketplace, exact: true, secure: true, scopes: [] }
];

export default routes;
