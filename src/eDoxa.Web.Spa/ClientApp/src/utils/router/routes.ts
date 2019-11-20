import React from "react";
import { RouteConfig } from "./types";

//const Home = React.lazy(() => import("views/Home/Home"));
const TermsOfServices = React.lazy(() => import("views/TermsOfServices"));
const FAQ = React.lazy(() => import("views/FAQ"));
const Marketplace = React.lazy(() => import("views/Marketplace/Marketplace"));
const NewsFeeds = React.lazy(() => import("views/NewsFeeds/NewsFeeds"));
const Profile = React.lazy(() => import("views/User/Profile"));
const Arena = React.lazy(() => import("views/Arena/Arena"));
const ArenaChallenges = React.lazy(() => import("views/Arena/Challenges/Challenges"));
const ArenaChallengeDetails = React.lazy(() => import("views/Arena/Challenges/Details/Details"));
const ArenaChallengeHistory = React.lazy(() => import("views/Arena/Challenges/History/History"));
const ArenaGames = React.lazy(() => import("views/Arena/Games/Games"));
const ArenaTournaments = React.lazy(() => import("views/Arena/Tournaments/Tournaments"));
const ArenaTournamentDetails = React.lazy(() => import("views/Arena/Tournaments/Details/Details"));
const ArenaTournamentHistory = React.lazy(() => import("views/Arena/Tournaments/History/History"));
const StructureClans = React.lazy(() => import("views/Organizations/Clans/Clans"));
const StructureClanDashboard = React.lazy(() => import("views/Organizations/Clans/Dashboard/Dashboard"));
const StructureClanDetails = React.lazy(() => import("views/Organizations/Clans/Details/Details"));
const StructureClanMarketplace = React.lazy(() => import("views/Organizations/Clans/Marketplace/Marketplace"));
const StructureLeagues = React.lazy(() => import("views/Organizations/Leagues/Leagues"));
const StructureLeagueDashboard = React.lazy(() => import("views/Organizations/Leagues/Dashboard/Dashboard"));
const StructureLeagueDetails = React.lazy(() => import("views/Organizations/Leagues/Details/Details"));
const StructureLeagueMarketplace = React.lazy(() => import("views/Organizations/Leagues/Makertplace/Marketplace"));
const StructureTeams = React.lazy(() => import("views/Organizations/Teams/Teams"));
const StructureTeamDashboard = React.lazy(() => import("views/Organizations/Teams/Dashboard/Dashboard"));
const StructureTeamDetails = React.lazy(() => import("views/Organizations/Teams/Details/Details"));
const StructureTeamMarketplace = React.lazy(() => import("views/Organizations/Teams/Makertplace/Marketplace"));

// https://github.com/ReactTraining/react-router/tree/master/packages/react-router-config
export const routes: RouteConfig[] = [
  { path: "/", name: "Home", component: ArenaChallenges, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: "/terms-of-services", name: "Terms of Services", component: TermsOfServices, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: "/faq", name: "FAQ", component: FAQ, exact: true, allowAnonymous: true, disabled: true, scopes: [] },
  { path: "/marketplace", name: "Marketplace", component: Marketplace, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: "/news-feeds", name: "News Feeds", component: NewsFeeds, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: "/profile", name: "Profile Overview", component: Profile, exact: false, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/arena", name: "Arena", component: Arena, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: "/arena/challenges", name: "Challenges", component: ArenaChallenges, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: "/arena/challenges/:challengeId", name: "Challenge Details", component: ArenaChallengeDetails, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/arena/challenge-history", name: "Challenge History", component: ArenaChallengeHistory, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/arena/games", name: "Games", component: ArenaGames, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/arena/tournaments", name: "Tournaments", component: ArenaTournaments, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/arena/tournaments/:tournamentId", name: "Tournament Details", component: ArenaTournamentDetails, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/arena/tournament-history", name: "Tournament History", component: ArenaTournamentHistory, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/clans", name: "Clans", component: StructureClans, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/clans/:clanId", name: "Clan Details", component: StructureClanDetails, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/clans/:clanId/dashboard", name: "Clan Dashboard", component: StructureClanDashboard, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/clans/:clanId/marketplace", name: "Clan Marketplace", component: StructureClanMarketplace, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/leagues", name: "Leagues", component: StructureLeagues, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: "/structures/leagues/:leagueId", name: "League Details", component: StructureLeagueDetails, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/leagues/:leagueId/dashboard", name: "League Dashboard", component: StructureLeagueDashboard, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/leagues/:leagueId/marketplace", name: "League Marketplace", component: StructureLeagueMarketplace, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/teams", name: "Teams", component: StructureTeams, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: "/structures/teams/:teamId", name: "Team Details", component: StructureTeamDetails, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/teams/:teamId/dashboard", name: "Team Dashboard", component: StructureTeamDashboard, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: "/structures/teams/:teamId/marketplace", name: "Team Marketplace", component: StructureTeamMarketplace, exact: true, allowAnonymous: false, disabled: false, scopes: [] }
];
