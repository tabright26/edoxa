import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Games } from "types";

export const GENERATE_GAME_AUTH_FACTOR = "GENERATE_GAME_AUTH_FACTOR";
export const GENERATE_GAME_AUTH_FACTOR_SUCCESS = "GENERATE_GAME_AUTH_FACTOR_SUCCESS";
export const GENERATE_GAME_AUTH_FACTOR_FAIL = "GENERATE_GAME_AUTH_FACTOR_FAIL";

export type GenerateGameAuthFactorType = typeof GENERATE_GAME_AUTH_FACTOR | typeof GENERATE_GAME_AUTH_FACTOR_SUCCESS | typeof GENERATE_GAME_AUTH_FACTOR_FAIL;
export type GenerateGameAuthFactorActionCreator = AxiosActionCreator<GenerateGameAuthFactorType>;
export type GenerateGameAuthFactorAction = AxiosAction<GenerateGameAuthFactorType>;

export type GameAuthFactorActionCreators = GenerateGameAuthFactorActionCreator;
export type GameAuthFactorActions = GenerateGameAuthFactorAction;
export type GameAuthFactorState = AxiosState<Games>;
