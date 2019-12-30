import { ChallengeId, ClanId } from "types";

export function getHomePath(): string {
  return "/";
}

export function getTermsOfServicesPath(): string {
  return "/terms-of-services";
}

export function getFaqPath(): string {
  return "/faq";
}

export function getNewsFeedsPath(): string {
  return "/news-feeds";
}

export function getUserProfilePath(): string {
  return "/user/profile";
}

export function getUserProfileOverviewPath(): string {
  return getUserProfilePath() + "/overview";
}

export function getUserProfileDetailsPath(): string {
  return getUserProfilePath() + "/details";
}

export function getUserProfileSecurityPath(): string {
  return getUserProfilePath() + "/security";
}

export function getUserProfileTransactionHistoryPath(): string {
  return getUserProfilePath() + "/transaction-history";
}

export function getUserProfilePaymentMethodsPath(): string {
  return getUserProfilePath() + "/payment-methods";
}

export function getUserProfileGamesPath(): string {
  return getUserProfilePath() + "/games";
}

export function getChallengesPath(): string {
  return "/challenges";
}

export function getChallengeDetailsPath(
  challengeId: ChallengeId = null
): string {
  return `/challenges/${challengeId === null ? ":challengeId" : challengeId}`;
}

export function getChallengeHistoryPath(): string {
  return "/challenge-history";
}

export function getClansPath(): string {
  return "/clans";
}

export function getClanDetailsPath(clanId: ClanId = null): string {
  return `/clans/${clanId === null ? ":clanId" : clanId}`;
}

export function getClanDashboardPath(clanId: ClanId = null): string {
  return `/clans/${clanId === null ? ":clanId" : clanId}/dashboard`;
}
