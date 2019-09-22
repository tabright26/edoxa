import { IAxiosActionCreator } from "interfaces/axios";
import { LoadChallengesActionType, LoadChallengeActionType } from "./actionTypes";

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
