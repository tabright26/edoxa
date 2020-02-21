import {
  Challenge,
  Entries,
  ChallengeId,
  CHALLENGE_STATE_INSCRIPTION,
  BestOf,
  ChallengeName,
  ChallengeState,
  ChallengeParticipant,
  ChallengeParticipantId,
  ChallengeMatch,
  Score
} from "types/challenges";
import { CURRENCY_TYPE_TOKEN } from "types/cashier";
import { Game, GAME_LEAGUEOFLEGENDS } from "types/games";
import { UserId } from "types/identity";

export const createChallenge = (
  id: ChallengeId = "ChallengeId",
  name: ChallengeName = "ChallengeName",
  game: Game = GAME_LEAGUEOFLEGENDS,
  state: ChallengeState = CHALLENGE_STATE_INSCRIPTION,
  bestOf: BestOf = 3,
  entries: Entries = 10,
  participants: ChallengeParticipant[] = []
): Challenge => {
  return {
    id,
    name,
    state,
    bestOf,
    entries,
    game,
    timeline: {
      duration: 84000,
      createdAt: 100000000,
      startedAt: null,
      endedAt: null,
      closedAt: null
    },
    scoring: new Map<string, string>(),
    payout: {
      challengeId: id,
      entries: entries / 2,
      entryFee: {
        type: CURRENCY_TYPE_TOKEN,
        amount: 0
      },
      prizePool: {
        type: CURRENCY_TYPE_TOKEN,
        amount: 200000
      },
      buckets: []
    },
    participants
  };
};

export const createChallengeParticipant = (
  challengeId: ChallengeId,
  participantId: ChallengeParticipantId = "ParticipantId",
  userId: UserId = "UserId",
  score: Score = 0,
  matches: ChallengeMatch[] = []
): ChallengeParticipant => {
  return {
    id: participantId,
    challengeId,
    score,
    userId,
    matches
  };
};
