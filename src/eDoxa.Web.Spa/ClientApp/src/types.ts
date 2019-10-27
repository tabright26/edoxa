import { Stream } from "stream";
import { Country } from "utils/localize/types";

export const MONEY = "money";
export const TOKEN = "token";

export type Currency = typeof MONEY | typeof TOKEN;

export const TRANSACTION_TYPE_DEPOSIT = "deposit";
export const TRANSACTION_TYPE_REWARD = "reward";
export const TRANSACTION_TYPE_CHARGE = "charge";
export const TRANSACTION_TYPE_PAYOUT = "payout";
export const TRANSACTION_TYPE_WITHDRAWAL = "withdrawal";

export type TransactionType = typeof TRANSACTION_TYPE_DEPOSIT | typeof TRANSACTION_TYPE_REWARD | typeof TRANSACTION_TYPE_CHARGE | typeof TRANSACTION_TYPE_PAYOUT | typeof TRANSACTION_TYPE_WITHDRAWAL;

export const TRANSACTION_STATUS_PENDING = "pending";
export const TRANSACTION_STATUS_SUCCEDED = "succeded";
export const TRANSACTION_STATUS_FAILED = "failed";

export type TransactionStatus = typeof TRANSACTION_STATUS_PENDING | typeof TRANSACTION_STATUS_SUCCEDED | typeof TRANSACTION_STATUS_FAILED;

export type AccountId = string;
export type AddressId = string;
export type CandidatureId = string;
export type ChallengeId = string;
export type ClanId = string;
export type InvitationId = string;
export type MatchId = string;
export type MemberId = string;
export type ParticipantId = string;
export type TransactionId = string;
export type UserId = string;

interface Entity<TEntityId> {
  readonly id: TEntityId;
}

export interface Transaction extends Entity<TransactionId> {
  readonly timestamp: number;
  readonly currency: Currency;
  readonly amount: number;
  readonly description: string;
  readonly type: TransactionType;
  readonly status: TransactionStatus;
}

export interface Bundle {
  readonly amount: number;
  readonly price: number;
}

export interface Balance {
  readonly available: number;
  readonly pending: number;
}

export type Gender = "Male" | "Female" | "Other";

export interface Dob {
  readonly year: number;
  readonly month: number;
  readonly day: number;
}

export interface Informations {
  readonly firstName: string;
  readonly lastName: string;
  readonly gender: Gender;
  readonly dob: Dob;
}

export interface Email {
  readonly address: string;
  readonly verified: boolean;
}

export interface Phone {
  readonly number: string;
  readonly verified: boolean;
}

export interface Address extends Entity<AddressId> {
  readonly country: Country | string;
  readonly line1: string;
  readonly line2?: string;
  readonly city: string;
  readonly state?: string;
  readonly postalCode?: string;
}

export interface Doxatag {
  readonly userId: UserId;
  readonly name: string;
  readonly code: number;
  readonly timestamp: number;
}

export interface Game {}

export type Logo = Stream | null;

export interface ClanOwner {
  readonly userId: UserId;
  readonly doxatag?: Doxatag;
}

export interface Clan extends Entity<ClanId> {
  readonly name: string;
  readonly ownerId: UserId;
  readonly owner?: ClanOwner;
  readonly members: Member[];
  readonly logo: Logo;
}

export interface Member extends Entity<MemberId> {
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: Doxatag;
}

export interface Candidature extends Entity<CandidatureId> {
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: Doxatag;
  readonly clan?: Clan;
}

export interface Invitation extends Entity<InvitationId> {
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: Doxatag;
  readonly clan?: Clan;
}

export interface ChallengeTimeline {
  readonly createdAt: number;
  readonly startedAt?: number;
  readonly endedAt?: number;
  readonly closedAt?: number;
}

export type ChallengeScoring = Map<string, string>;

export interface Challenge extends Entity<ChallengeId> {
  readonly timestamp: number;
  readonly timeline: ChallengeTimeline;
  readonly scoring?: ChallengeScoring;
  readonly participants: Participant[];
}

export interface Participant extends Entity<ParticipantId> {
  readonly matches: Match[];
}

export interface Match extends Entity<MatchId> {
  readonly stats: Stat[];
}

export interface Stat {}

// Stripe
export const STRIPE_CARD_TYPE = "card";

export type StripePaymentMethodType = typeof STRIPE_CARD_TYPE;

export type StripePaymentMethodId = string;

export interface StripeCustomer {
  readonly defaultPaymentMethodId: StripePaymentMethodId;
}

export interface StripePaymentMethod {
  readonly id: StripePaymentMethodId;
  readonly type: StripePaymentMethodType;
  readonly card: StripeCard;
}

export interface StripeCard {
  readonly brand: string;
  readonly country: string;
  readonly expMonth: number;
  readonly expYear: number;
  readonly last4: string;
}

export interface StripeAccount {
  readonly enabled: boolean;
}

export interface StripeBankAccount {
  readonly bankName: string;
  readonly country: string;
  readonly currency: string;
  readonly last4: string;
  readonly status: string;
  readonly defaultForCurrency: boolean;
}