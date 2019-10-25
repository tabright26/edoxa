import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Challenge } from "types";

export const LOAD_CHALLENGES = "LOAD_CHALLENGES";
export const LOAD_CHALLENGES_SUCCESS = "LOAD_CHALLENGES_SUCCESS";
export const LOAD_CHALLENGES_FAIL = "LOAD_CHALLENGES_FAIL";

export const LOAD_CHALLENGE = "LOAD_CHALLENGE";
export const LOAD_CHALLENGE_SUCCESS = "LOAD_CHALLENGE_SUCCESS";
export const LOAD_CHALLENGE_FAIL = "LOAD_CHALLENGE_FAIL";

export type LoadChallengesType = typeof LOAD_CHALLENGES | typeof LOAD_CHALLENGES_SUCCESS | typeof LOAD_CHALLENGES_FAIL;
export type LoadChallengesActionCreator = AxiosActionCreator<LoadChallengesType>;
export type LoadChallengesAction = AxiosAction<LoadChallengesType>;

export type LoadChallengeType = typeof LOAD_CHALLENGE | typeof LOAD_CHALLENGE_SUCCESS | typeof LOAD_CHALLENGE_FAIL;
export type LoadChallengeActionCreator = AxiosActionCreator<LoadChallengeType>;
export type LoadChallengeAction = AxiosAction<LoadChallengeType>;

export type ChallengesActionCreators = LoadChallengesActionCreator | LoadChallengeActionCreator;
export type ChallengesActions = LoadChallengesAction | LoadChallengeAction;
export type ChallengesState = AxiosState<Challenge[]>;
