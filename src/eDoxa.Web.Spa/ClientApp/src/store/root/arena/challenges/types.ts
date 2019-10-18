import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";
import { Challenge } from "types";

export const LOAD_CHALLENGES = "LOAD_CHALLENGES";
export const LOAD_CHALLENGES_SUCCESS = "LOAD_CHALLENGES_SUCCESS";
export const LOAD_CHALLENGES_FAIL = "LOAD_CHALLENGES_FAIL";

export const LOAD_CHALLENGE = "LOAD_CHALLENGE";
export const LOAD_CHALLENGE_SUCCESS = "LOAD_CHALLENGE_SUCCESS";
export const LOAD_CHALLENGE_FAIL = "LOAD_CHALLENGE_FAIL";

type LoadChallengesType = typeof LOAD_CHALLENGES | typeof LOAD_CHALLENGES_SUCCESS | typeof LOAD_CHALLENGES_FAIL;

interface LoadChallengesActionCreator extends AxiosActionCreator<LoadChallengesType> {}

interface LoadChallengesAction extends AxiosAction<LoadChallengesType> {}

type LoadChallengeType = typeof LOAD_CHALLENGE | typeof LOAD_CHALLENGE_SUCCESS | typeof LOAD_CHALLENGE_FAIL;

interface LoadChallengeActionCreator extends AxiosActionCreator<LoadChallengeType> {}

interface LoadChallengeAction extends AxiosAction<LoadChallengeType> {}

export type ChallengesActionCreators = LoadChallengesActionCreator | LoadChallengeActionCreator;

export type ChallengesActions = LoadChallengesAction | LoadChallengeAction;

export type ChallengesState = AxiosState<Challenge[]>;
