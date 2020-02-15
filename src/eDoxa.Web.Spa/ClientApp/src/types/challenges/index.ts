import { EntryFee, Currency } from "types/cashier";
import { Game } from "types/games";
import { UserId } from "types/identity";

export type ChallengeId = string;
export type MatchId = string;
export type ParticipantId = string;

export interface Challenge {
  readonly id: ChallengeId;
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

export type ChallengeScoring = Map<string, string>;

export interface ChallengePayout {
  readonly challengeId: ChallengeId;
  readonly entries: number;
  readonly entryFee: EntryFee;
  readonly prizePool: ChallengePayoutPrizePool;
  readonly buckets: ChallengePayoutBucket[];
}

export type ChallengePayoutPrizePool = Currency;

export interface ChallengePayoutBucket {
  readonly size: number;
  readonly prize: number;
}

export interface ChallengeParticipant {
  readonly id: ParticipantId;
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

export interface ChallengeParticipantMatch {
  readonly id: MatchId;
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
