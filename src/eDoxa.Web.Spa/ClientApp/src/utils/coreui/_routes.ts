import React from "react";
import { RouteConfig } from "../router/types";
import {
  getHomePath,
  getTermsOfServicesPath, 
  getFaqPath,
  getNewsFeedsPath,
  getUserProfilePath,
  getChallengesPath,
  getChallengeDetailsPath,
  getChallengeHistoryPath,
  getClansPath,
  getClanDetailsPath,
  getClanDashboardPath
} from "utils/coreui/constants";

//const Home = React.lazy(() => import("views/Home/Home"));
const TermsOfServices = React.lazy(() => import("views/TermsOfServices"));
const FAQ = React.lazy(() => import("views/FAQ"));
const NewsFeeds = React.lazy(() => import("views/NewsFeeds"));
const Profile = React.lazy(() => import("views/User/Profile"));
const Challenges = React.lazy(() => import("views/Challenges"));
const ChallengeDetails = React.lazy(() => import("views/Challenges/Details"));
const ChallengeHistory = React.lazy(() => import("views/Challenges/History"));
const Clans = React.lazy(() => import("views/Clans"));
const ClanDashboard = React.lazy(() => import("views/Clans/Dashboard"));
const ClanDetails = React.lazy(() => import("views/Clans/Details"));

// https://github.com/ReactTraining/react-router/tree/master/packages/react-router-config
export const routes: RouteConfig[] = [
  { path: getHomePath(), name: "Home", component: Challenges, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: getTermsOfServicesPath(), name: "Terms of Services", component: TermsOfServices, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: getFaqPath(), name: "FAQ", component: FAQ, exact: true, allowAnonymous: true, disabled: true, scopes: [] },
  { path: getNewsFeedsPath(), name: "News Feeds", component: NewsFeeds, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: getChallengesPath(), name: "Challenges", component: Challenges, exact: true, allowAnonymous: true, disabled: false, scopes: [] },
  { path: getChallengeDetailsPath(), name: "Challenge Details", component: ChallengeDetails, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: getChallengeHistoryPath(), name: "Challenge History", component: ChallengeHistory, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: getClansPath(), name: "Clans", component: Clans, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: getClanDetailsPath(), name: "Clan Details", component: ClanDetails, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: getClanDashboardPath(), name: "Clan Dashboard", component: ClanDashboard, exact: true, allowAnonymous: false, disabled: false, scopes: [] },
  { path: getUserProfilePath(), name: "User Profile Overview", component: Profile, exact: false, allowAnonymous: false, disabled: false, scopes: [] }
];
