import { ChallengeId, ClanId } from "types";

export function getHomePath(): string {
  return "/";
}

function getErrorPath(code: number): string {
  return `/errors/${code}`;
}

export function getError401Path(): string {
  return getErrorPath(401);
}

export function getError403Path(): string {
  return getErrorPath(403);
}

export function getError404Path(): string {
  return getErrorPath(404);
}

export function getError500Path(): string {
  return getErrorPath(500);
}

export function getEmailConfirmPath(): string {
  return "/email/confirm";
}

export function getPasswordForgotPath(): string {
  return "/password/forgot";
}

export function getPasswordResetPath(): string {
  return "/password/reset";
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

export function getUserProfilePromotionalCodePath(): string {
  return getUserProfilePath() + "/promotional-code";
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
