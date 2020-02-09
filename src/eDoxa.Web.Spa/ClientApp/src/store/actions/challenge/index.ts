import {
  LOAD_CHALLENGES,
  LOAD_CHALLENGES_SUCCESS,
  LOAD_CHALLENGES_FAIL,
  LOAD_CHALLENGE,
  LOAD_CHALLENGE_SUCCESS,
  LOAD_CHALLENGE_FAIL,
  REGISTER_CHALLENGE_PARTICIPANT,
  REGISTER_CHALLENGE_PARTICIPANT_SUCCESS,
  REGISTER_CHALLENGE_PARTICIPANT_FAIL,
  LOAD_CHALLENGE_HISTORY,
  LOAD_CHALLENGE_HISTORY_SUCCESS,
  LOAD_CHALLENGE_HISTORY_FAIL,
  LoadChallengesActionCreator,
  LoadChallengeActionCreator,
  LoadChallengeHistoryActionCreator,
  RegisterChallengeParticipantActionCreator
} from "./types";
import {
  AXIOS_PAYLOAD_CLIENT_CHALLENGES,
  AxiosActionCreatorMeta
} from "utils/axios/types";
import { ChallengeId, Game, ChallengeState } from "types";

export function loadChallenges(
  game: Game = null,
  state: ChallengeState = null
): LoadChallengesActionCreator {
  return {
    types: [LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CHALLENGES,
      request: {
        method: "GET",
        url: "/api/challenges",
        params: {
          game,
          state
        }
      }
    }
  };
}

export function loadChallenge(
  challengeId: ChallengeId
): LoadChallengeActionCreator {
  return {
    types: [LOAD_CHALLENGE, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CHALLENGES,
      request: {
        method: "GET",
        url: `/api/challenges/${challengeId}`
      }
    }
  };
}

export function loadChallengeHistory(
  game: Game = null,
  state: ChallengeState = null
): LoadChallengeHistoryActionCreator {
  return {
    types: [
      LOAD_CHALLENGE_HISTORY,
      LOAD_CHALLENGE_HISTORY_SUCCESS,
      LOAD_CHALLENGE_HISTORY_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CHALLENGES,
      request: {
        method: "GET",
        url: "/api/challenge-history",
        params: {
          game,
          state
        }
      }
    }
  };
}

export function registerChallengeParticipant(
  challengeId: ChallengeId,
  meta: AxiosActionCreatorMeta
): RegisterChallengeParticipantActionCreator {
  return {
    types: [
      REGISTER_CHALLENGE_PARTICIPANT,
      REGISTER_CHALLENGE_PARTICIPANT_SUCCESS,
      REGISTER_CHALLENGE_PARTICIPANT_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CHALLENGES,
      request: {
        method: "POST",
        url: `/api/challenges/${challengeId}/participants`
      }
    },
    meta
  };
}
