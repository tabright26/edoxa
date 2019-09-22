import { IAxiosActionCreator } from "interfaces/axios";

export const LOAD_CHALLENGES = "LOAD_CHALLENGES";
export const LOAD_CHALLENGES_SUCCESS = "LOAD_CHALLENGES_SUCCESS";
export const LOAD_CHALLENGES_FAIL = "LOAD_CHALLENGES_FAIL";
export type LoadChallengesActionType = "LOAD_CHALLENGES" | "LOAD_CHALLENGES_SUCCESS" | "LOAD_CHALLENGES_FAIL";
export function loadChallenges(): IAxiosActionCreator<LoadChallengesActionType> {
  return {
    types: ["LOAD_CHALLENGES", "LOAD_CHALLENGES_SUCCESS", "LOAD_CHALLENGES_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: "/arena/challenge/api/challenges"
      }
    }
  };
}

export const LOAD_CHALLENGE = "LOAD_CHALLENGE";
export const LOAD_CHALLENGE_SUCCESS = "LOAD_CHALLENGE_SUCCESS";
export const LOAD_CHALLENGE_FAIL = "LOAD_CHALLENGE_FAIL";
export type LoadChallengeActionType = "LOAD_CHALLENGE" | "LOAD_CHALLENGE_SUCCESS" | "LOAD_CHALLENGE_FAIL";
export function loadChallenge(challengeId: string): IAxiosActionCreator<LoadChallengeActionType> {
  return {
    types: ["LOAD_CHALLENGE", "LOAD_CHALLENGE_SUCCESS", "LOAD_CHALLENGE_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: `/arena/challenge/api/challenges/${challengeId}`
      }
    }
  };
}
