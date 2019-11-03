import { LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL, LOAD_CHALLENGE, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL, ChallengesActionCreators } from "./types";

export function loadChallenges(): ChallengesActionCreators {
  return {
    types: [LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/challenge/api/challenges"
      }
    }
  };
}

export function loadChallenge(challengeId: string): ChallengesActionCreators {
  return {
    types: [LOAD_CHALLENGE, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/challenge/api/challenges/${challengeId}`
      }
    }
  };
}
