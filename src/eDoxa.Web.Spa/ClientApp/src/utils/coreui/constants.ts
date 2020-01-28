import { ChallengeId, ClanId } from "types";

export function getHomePath(): string {
  return "/";
}

function getErrorPath(httpCode: number): string {
  return `/errors/${httpCode}`;
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

function getAccountPath(): string {
  return "/account";
}

export function getAccountRegisterPath(): string {
  return getAccountPath() + "/register";
}

export function getAccountLogoutPath(): string {
  return getAccountPath() + "/logout";
}

export function getAccountLoginPath(): string {
  return getAccountPath() + "/login";
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

export function getProfilePath(): string {
  return "/profile";
}

export function getProfileOverviewPath(): string {
  return getProfilePath() + "/overview";
}

export function getProfileDetailsPath(): string {
  return getProfilePath() + "/details";
}

export function getProfileSecurityPath(): string {
  return getProfilePath() + "/security";
}

export function getProfileTransactionHistoryPath(): string {
  return getProfilePath() + "/transaction-history";
}

export function getProfilePaymentMethodsPath(): string {
  return getProfilePath() + "/payment-methods";
}

export function getProfilePromotionalCodePath(): string {
  return getProfilePath() + "/promotional-code";
}

export function getProfileGamesPath(): string {
  return getProfilePath() + "/games";
}

export function getProfileChallengeHistoryPath(): string {
  return getProfilePath() + "/challenge-history";
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
