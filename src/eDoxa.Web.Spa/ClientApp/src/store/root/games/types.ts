import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Games } from "types";

export const LOAD_GAMES = "LOAD_GAMES";
export const LOAD_GAMES_SUCCESS = "LOAD_GAMES_SUCCESS";
export const LOAD_GAMES_FAIL = "LOAD_GAMES_FAIL";

export type LoadGamesType = typeof LOAD_GAMES | typeof LOAD_GAMES_SUCCESS | typeof LOAD_GAMES_FAIL;
export type LoadGamesActionCreator = AxiosActionCreator<LoadGamesType>;
export type LoadGamesAction = AxiosAction<LoadGamesType>;

export type GamesActionCreators = LoadGamesActionCreator;
export type GamesActions = LoadGamesAction;
export type GamesState = AxiosState<Games>;
