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
  ChallengesActionCreators
} from "./types";

import { ChallengeId } from "types";
import { AXIOS_PAYLOAD_CLIENT_CHALLENGES } from "utils/axios/types";

export function loadChallenges(): ChallengesActionCreators {
  return {
    types: [LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CHALLENGES,
      request: {
        method: "GET",
        url: "/api/challenges"
      }
    }
  };
}

export function loadChallenge(
  challengeId: ChallengeId
): ChallengesActionCreators {
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

export function registerChallengeParticipant(
  challengeId: ChallengeId
): ChallengesActionCreators {
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
    }
  };
}
