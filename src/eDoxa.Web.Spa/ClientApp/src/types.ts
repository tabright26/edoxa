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

export type TransactionType =
  | typeof TRANSACTION_TYPE_DEPOSIT
  | typeof TRANSACTION_TYPE_REWARD
  | typeof TRANSACTION_TYPE_CHARGE
  | typeof TRANSACTION_TYPE_PAYOUT
  | typeof TRANSACTION_TYPE_WITHDRAWAL;

export const TRANSACTION_STATUS_PENDING = "pending";
export const TRANSACTION_STATUS_SUCCEDED = "succeded";
export const TRANSACTION_STATUS_FAILED = "failed";

export type TransactionStatus =
  | typeof TRANSACTION_STATUS_PENDING
  | typeof TRANSACTION_STATUS_SUCCEDED
  | typeof TRANSACTION_STATUS_FAILED;

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

export type Game = "LeagueOfLegends";

export type Logo = Stream | string | null;

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

// Game
export type Games = Map<Game, GameOption>;

export type GameServiceName = "manager" | "challenge" | "tournament";

export interface GameOption {
  readonly name: string;
  readonly displayName: string;
  readonly displayed: boolean;
  readonly verified: boolean;
  readonly services: Map<GameServiceName, boolean>;
}

export interface GameCredential {
  readonly userId: UserId;
  readonly game: Game;
}

// Challenge
export interface Challenge extends Entity<ChallengeId> {
  readonly name: string;
  readonly game: Game;
  readonly state: ChallengeState;
  readonly bestOf: number;
  readonly entries: number;
  readonly payoutEntries: number;
  readonly synchronizedAt?: number;
  readonly timeline: ChallengeTimeline;
  readonly entryFee: ChallengeEntryFee;
  readonly scoring: ChallengeScoring;
  readonly payout: ChallengePayout;
  readonly participants: ChallengeParticipant[];
}

export const CHALLENGE_STATE_INSCRIPTION = "Inscription";
export const CHALLENGE_STATE_STARTED = "Started";
export const CHALLENGE_STATE_ENDED = "Ended";
export const CHALLENGE_STATE_CLOSED = "Closed";

export type ChallengeState =
  | typeof CHALLENGE_STATE_INSCRIPTION
  | typeof CHALLENGE_STATE_STARTED
  | typeof CHALLENGE_STATE_ENDED
  | typeof CHALLENGE_STATE_CLOSED;

export interface ChallengeTimeline {
  readonly createdAt: number;
  readonly startedAt?: number;
  readonly endedAt?: number;
  readonly closedAt?: number;
}

export interface ChallengeEntryFee {
  readonly currency: Currency;
  readonly amount: number;
}

export type ChallengeScoring = Map<string, string>;

export interface ChallengePayout {
  readonly prizePool: ChallengePayoutPrizePool;
  readonly buckets: ChallengePayoutBucket[];
}

export interface ChallengePayoutPrizePool {
  readonly currency: Currency;
  readonly amount: number;
}

export interface ChallengePayoutBucket {
  readonly size: number;
  readonly prize: number;
}

export interface ChallengeParticipant extends Entity<ParticipantId> {
  readonly score: number;
  readonly challengeId: ChallengeId;
  readonly user: ChallengeParticipantUser;
  readonly matches: ChallengeParticipantMatch[];
}

export interface ChallengeParticipantUser {
  readonly id: UserId;
  readonly doxatag?: ChallengeParticipantUserDoxatag;
}

export interface ChallengeParticipantUserDoxatag {
  readonly name: string;
  readonly code: number;
}

export interface ChallengeParticipantMatch extends Entity<MatchId> {
  readonly score: number;
  readonly participantId: ParticipantId;
  readonly challengeId: ChallengeId;
  readonly stats: ChallengeParticipantMatchStat[];
}

export interface ChallengeParticipantMatchStat {
  readonly name: string;
  readonly value: number;
  readonly weighting: number;
  readonly score: number;
}

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
