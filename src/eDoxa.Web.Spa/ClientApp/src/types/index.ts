export const GAME_LEAGUE_OF_LEGENDS = "LeagueOfLegends";

export type Game = typeof GAME_LEAGUE_OF_LEGENDS;

export const GENDER_MALE = "Male";
export const GENDER_FEMALE = "Female";
export const GENDER_OTHER = "Other";

export type Gender =
  | typeof GENDER_MALE
  | typeof GENDER_FEMALE
  | typeof GENDER_OTHER;

export const CURRENCY_TYPE_MONEY = "money";
export const CURRENCY_TYPE_TOKEN = "token";
export const CURRENCY_TYPE_ALL = "all";

export type CurrencyType =
  | typeof CURRENCY_TYPE_MONEY
  | typeof CURRENCY_TYPE_TOKEN
  | typeof CURRENCY_TYPE_ALL;

export const TRANSACTION_TYPE_DEPOSIT = "Deposit";
export const TRANSACTION_TYPE_REWARD = "Reward";
export const TRANSACTION_TYPE_CHARGE = "Charge";
export const TRANSACTION_TYPE_PAYOUT = "Payout";
export const TRANSACTION_TYPE_WITHDRAWAL = "Withdrawal";
export const TRANSACTION_TYPE_PROMOTION = "Promotion";
export const TRANSACTION_TYPE_ALL = "All";

export type TransactionType =
  | typeof TRANSACTION_TYPE_DEPOSIT
  | typeof TRANSACTION_TYPE_REWARD
  | typeof TRANSACTION_TYPE_CHARGE
  | typeof TRANSACTION_TYPE_PAYOUT
  | typeof TRANSACTION_TYPE_WITHDRAWAL
  | typeof TRANSACTION_TYPE_PROMOTION
  | typeof TRANSACTION_TYPE_ALL;

export const TRANSACTION_STATUS_PENDING = "Pending";
export const TRANSACTION_STATUS_SUCCEEDED = "Succeeded";
export const TRANSACTION_STATUS_FAILED = "Failed";

export interface LogoutToken {
  readonly logoutId?: string;
  readonly clientName?: string;
  readonly postLogoutRedirectUri?: string;
  readonly signOutIFrameUrl?: string;
  readonly showSignoutPrompt: boolean;
}

export type TransactionStatus =
  | typeof TRANSACTION_STATUS_PENDING
  | typeof TRANSACTION_STATUS_SUCCEEDED
  | typeof TRANSACTION_STATUS_FAILED;

export const COUNTRY_ISO_CODE_CA = "CA";
export const COUNTRY_ISO_CODE_US = "US";

export type CountryIsoCode =
  | typeof COUNTRY_ISO_CODE_CA
  | typeof COUNTRY_ISO_CODE_US;

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

export interface Currency {
  readonly amount: number;
  readonly type: CurrencyType;
}

interface Entity<TEntityId> {
  readonly id: TEntityId;
}

export interface ChallengesStaticOptions {}

export interface GamesStaticOptions {
  readonly games: GameOptions[];
}

export interface CashierStaticOptions {
  readonly transaction: TransactionOptions;
  readonly promotion: PromotionOptions;
}

export interface PromotionOptions {}

export interface TransactionOptions {
  readonly bundles: TransactionBundle[];
}

export interface TransactionBundle {
  readonly id: TransactionBundleId;
  readonly currency: Currency;
  readonly price: Currency;
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

export interface StripeOptions {
  readonly currencies: CurrenciesOptions;
  readonly paymentMethod: PaymentMethodOptions;
}

export interface CurrenciesOptions {
  readonly ca: string[];
  readonly us: string[];
}

export interface PaymentMethodOptions {
  readonly card: CardOptions;
}

export interface CardOptions {
  readonly limit: number;
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
export type GameServiceName = "Game" | "Challenge" | "Tournament";

export interface GameOptions {
  readonly name: Game;
  readonly displayName: string;
  readonly disabled: boolean;
  readonly services: GameServiceOptions[];
}

export interface GameServiceOptions {
  readonly name: GameServiceName;
  readonly disabled: boolean;
  readonly instructions: string;
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
  readonly synchronizedAt?: number;
  readonly timeline: ChallengeTimeline;
  readonly scoring: ChallengeScoring;
  readonly payout: ChallengePayout;
  readonly participants: ChallengeParticipant[];
}

export const CHALLENGE_STATE_INSCRIPTION = "Inscription";
export const CHALLENGE_STATE_STARTED = "InProgress";
export const CHALLENGE_STATE_ENDED = "Ended";
export const CHALLENGE_STATE_CLOSED = "Closed";

export type ChallengeState =
  | typeof CHALLENGE_STATE_INSCRIPTION
  | typeof CHALLENGE_STATE_STARTED
  | typeof CHALLENGE_STATE_ENDED
  | typeof CHALLENGE_STATE_CLOSED;

export interface ChallengeTimeline {
  readonly duration: number;
  readonly createdAt: number;
  readonly startedAt?: number;
  readonly endedAt?: number;
  readonly closedAt?: number;
}

export interface EntryFee extends Currency {}

export type ChallengeScoring = Map<string, string>;

export interface ChallengePayout {
  readonly challengeId: ChallengeId;
  readonly entries: number;
  readonly entryFee: EntryFee;
  readonly prizePool: ChallengePayoutPrizePool;
  readonly buckets: ChallengePayoutBucket[];
}

export interface ChallengePayoutPrizePool extends Currency {}

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
  readonly challengeId: ChallengeId;
  readonly participantId: ParticipantId;
  readonly gameUuid: string;
  readonly gameStartedAt: Date;
  readonly gameDuration: number;
  readonly gameEndedAt: Date;
  readonly synchronizedAt: Date;
  readonly isBestOf: boolean;
  readonly score: number;
  readonly stats: ChallengeParticipantMatchStat[];
}

export interface ChallengeParticipantMatchStat {
  readonly name: string;
  readonly value: number;
  readonly weighting: number;
  readonly score: number;
}

// Stripe
export type StripePaymentMethodId = string;

export interface StripeCustomer {
  readonly defaultPaymentMethodId: StripePaymentMethodId;
}

export interface StripePaymentMethod {
  readonly id: StripePaymentMethodId;
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
