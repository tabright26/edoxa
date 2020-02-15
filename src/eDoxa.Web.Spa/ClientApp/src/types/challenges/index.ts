import { EntryFee, PrizePool } from "types/cashier";
import { Game } from "types/games";
import { UserId, Doxatag } from "types/identity";

export const CHALLENGE_STATE_INSCRIPTION = "Inscription";
export const CHALLENGE_STATE_STARTED = "InProgress";
export const CHALLENGE_STATE_ENDED = "Ended";
export const CHALLENGE_STATE_CLOSED = "Closed";

export type ChallengeState =
  | typeof CHALLENGE_STATE_INSCRIPTION
  | typeof CHALLENGE_STATE_STARTED
  | typeof CHALLENGE_STATE_ENDED
  | typeof CHALLENGE_STATE_CLOSED;

export type ChallengeId = string;
export type ChallengeMatchId = string;
export type ChallengeParticipantId = string;
export type ChallengeScoring = Map<string, string>;
export type ChallengeName = string;
export type PayoutEntries = number;
export type Entries = number;
export type Score = number;
export type BestOf = number;

export interface Challenge {
  readonly id: ChallengeId;
  readonly name: ChallengeName;
  readonly game: Game;
  readonly state: ChallengeState;
  readonly bestOf: BestOf;
  readonly entries: Entries;
  readonly synchronizedAt?: number;
  readonly timeline: ChallengeTimeline;
  readonly scoring: ChallengeScoring;
  readonly payout: ChallengePayout;
  readonly participants: ChallengeParticipant[];
}

export interface ChallengeTimeline {
  readonly duration: number;
  readonly createdAt: number;
  readonly startedAt?: number;
  readonly endedAt?: number;
  readonly closedAt?: number;
}

export interface ChallengePayout {
  readonly challengeId: ChallengeId;
  readonly entries: PayoutEntries;
  readonly entryFee: EntryFee;
  readonly prizePool: PrizePool;
  readonly buckets: ChallengePayoutBucket[];
}

export interface ChallengePayoutBucket {
  readonly size: number;
  readonly prize: number;
}

export interface ChallengeParticipant {
  readonly id: ChallengeParticipantId;
  readonly challengeId: ChallengeId;
  readonly userId: UserId;
  readonly doxatag?: Doxatag;
  readonly score: Score;
  readonly matches: ChallengeMatch[];
}

export interface ChallengeMatch {
  readonly id: ChallengeMatchId;
  readonly participantId: ChallengeParticipantId;
  readonly challengeId: ChallengeId;
  readonly synchronizedAt: number;
  readonly score: Score;
  readonly gameUuid: string;
  readonly gameDuration: number;
  readonly gameStartedAt: number;
  readonly gameEndedAt: number;
  readonly isBestOf: boolean;
  readonly stats: ChallengeMatchStat[];
}

export interface ChallengeMatchStat {
  readonly name: string;
  readonly value: number;
  readonly weighting: number;
  readonly score: Score;
}
