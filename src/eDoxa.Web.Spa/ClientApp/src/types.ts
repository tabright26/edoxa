export const GAME_LEAGUE_OF_LEGENDS = "LeagueOfLegends";

export type Game = typeof GAME_LEAGUE_OF_LEGENDS;

export const GENDER_MALE = "Male";
export const GENDER_FEMALE = "Female";
export const GENDER_OTHER = "Other";

export type Gender =
  | typeof GENDER_MALE
  | typeof GENDER_FEMALE
  | typeof GENDER_OTHER;

export const CURRENCY_MONEY = "money";
export const CURRENCY_TOKEN = "token";
export const CURRENCY_ALL = "all";

export type Currency =
  | typeof CURRENCY_MONEY
  | typeof CURRENCY_TOKEN
  | typeof CURRENCY_ALL;

export const TRANSACTION_TYPE_DEPOSIT = "Deposit";
export const TRANSACTION_TYPE_REWARD = "Reward";
export const TRANSACTION_TYPE_CHARGE = "Charge";
export const TRANSACTION_TYPE_PAYOUT = "Payout";
export const TRANSACTION_TYPE_WITHDRAWAL = "Withdrawal";
export const TRANSACTION_TYPE_ALL = "All";

export type TransactionType =
  | typeof TRANSACTION_TYPE_DEPOSIT
  | typeof TRANSACTION_TYPE_REWARD
  | typeof TRANSACTION_TYPE_CHARGE
  | typeof TRANSACTION_TYPE_PAYOUT
  | typeof TRANSACTION_TYPE_WITHDRAWAL
  | typeof TRANSACTION_TYPE_ALL;

export const TRANSACTION_STATUS_PENDING = "Pending";
export const TRANSACTION_STATUS_SUCCEEDED = "Succeeded";
export const TRANSACTION_STATUS_FAILED = "Failed";

export type TransactionStatus =
  | typeof TRANSACTION_STATUS_PENDING
  | typeof TRANSACTION_STATUS_SUCCEEDED
  | typeof TRANSACTION_STATUS_FAILED;

export type CountryIsoCode = string;
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
export type TransactionBundleId = number;

interface Entity<TEntityId> {
  readonly id: TEntityId;
}

export interface TransactionBundle {
  readonly id: TransactionBundleId;
  readonly currency: {
    readonly amount: number;
    readonly type: Currency;
  };
  readonly price: {
    readonly amount: number;
    readonly type: Currency;
  };
  readonly type: TransactionType;
  readonly description: string;
  readonly notes: string;
  readonly disabled: boolean;
  readonly deprecated: boolean;
}

export interface Balance {
  readonly available: number;
  readonly pending: number;
}

export interface UserTransaction extends Entity<TransactionId> {
  readonly timestamp: number;
  readonly currency: Currency;
  readonly amount: number;
  readonly description: string;
  readonly type: TransactionType;
  readonly status: TransactionStatus;
}

export const FIELD_VALIDATION_RULE_TYPE_REQUIRED = "Required";
export const FIELD_VALIDATION_RULE_TYPE_REGEX = "Regex";
export const FIELD_VALIDATION_RULE_TYPE_LENGTH = "Length";
export const FIELD_VALIDATION_RULE_TYPE_MIN_LENGTH = "MinLength";
export const FIELD_VALIDATION_RULE_TYPE_MAX_LENGTH = "MaxLength";

export type FieldValidationRuleType =
  | typeof FIELD_VALIDATION_RULE_TYPE_REQUIRED
  | typeof FIELD_VALIDATION_RULE_TYPE_REGEX
  | typeof FIELD_VALIDATION_RULE_TYPE_LENGTH
  | typeof FIELD_VALIDATION_RULE_TYPE_MIN_LENGTH
  | typeof FIELD_VALIDATION_RULE_TYPE_MAX_LENGTH;

export interface FieldValidationRule {
  readonly type: FieldValidationRuleType;
  readonly message: string;
  readonly value: any;
  readonly enabled: boolean;
  readonly order: number;
}

// Identity
export interface IdentityStaticOptions {
  readonly addressBook: AddressBookOptions;
  readonly countries: CountryOptions[];
}

export interface AddressBookOptions {
  readonly limit: number;
}

export interface CountryOptions {
  readonly isoCode: CountryIsoCode;
  readonly name: string;
  readonly code: string;
  readonly address: AddressOptions;
  readonly regions: CountryRegionOptions[];
}

export interface CountryRegionOptions {
  readonly name: string;
  readonly code: string;
}

export interface AddressOptions {
  readonly fields: AddressFieldsOptions;
  readonly validator: AddressValidatorOptions;
}

export interface AddressFieldsOptions {
  readonly country: { readonly label: string; readonly placeholder: string };
  readonly line1: { readonly label: string; readonly placeholder: string };
  readonly line2: {
    readonly label: string;
    readonly placeholder: string;
    readonly excluded: boolean;
  };
  readonly city: { readonly label: string; readonly placeholder: string };
  readonly state: {
    readonly label: string;
    readonly placeholder: string;
    readonly excluded: boolean;
  };
  readonly postalCode: {
    readonly label: string;
    readonly placeholder: string;
    readonly excluded: boolean;
    readonly mask: string;
  };
}

export interface AddressValidatorOptions {
  readonly line1: FieldValidationRule[];
  readonly line2: FieldValidationRule[];
  readonly city: FieldValidationRule[];
  readonly state: FieldValidationRule[];
  readonly postalCode: FieldValidationRule[];
}

export interface UserEmail {
  readonly address: string;
  readonly verified: boolean;
}

export interface UserPhone {
  readonly number: string;
  readonly verified: boolean;
}

export interface UserProfile {
  readonly firstName: string;
  readonly lastName: string;
  readonly gender: Gender;
  readonly dob: UserDob;
}

export interface UserDob {
  readonly year: number;
  readonly month: number;
  readonly day: number;
}

export interface UserAddress extends Entity<AddressId> {
  readonly countryIsoCode: CountryIsoCode;
  readonly line1: string;
  readonly line2?: string;
  readonly city: string;
  readonly state?: string;
  readonly postalCode?: string;
}

export interface UserDoxatag {
  readonly userId: string;
  readonly name: string;
  readonly code: number;
  readonly timestamp: number;
}

export interface Clan extends Entity<ClanId> {
  readonly name: string;
  readonly ownerId: UserId;
  readonly owner?: ClanOwner;
  readonly members: ClanMember[];
  readonly logo: ClanLogo;
}

export type ClanLogo = string;

export interface ClanOwner {
  readonly userId: UserId;
  readonly doxatag?: UserDoxatag;
}

export interface ClanMember extends Entity<MemberId> {
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: UserDoxatag;
}

export interface ClanCandidature extends Entity<CandidatureId> {
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: UserDoxatag;
  readonly clan?: Clan;
}

export interface ClanInvitation extends Entity<InvitationId> {
  readonly clanId: ClanId;
  readonly userId: UserId;
  readonly doxatag?: UserDoxatag;
  readonly clan?: Clan;
}

// Game
export type Games = Map<Game, GameOption>;

export type GameServiceName = "manager" | "challenge" | "tournament";

export interface GameOption {
  readonly name: Game;
  readonly displayName: string;
  readonly displayed: boolean;
  readonly instructions: string;
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
  readonly challengeId: ChallengeId;
  readonly entryFee: ChallengeEntryFee;
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
