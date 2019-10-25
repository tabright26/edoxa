import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Game } from "types";

export const LOAD_USER_GAMES = "LOAD_USER_GAMES";
export const LOAD_USER_GAMES_SUCCESS = "LOAD_USER_GAMES_SUCCESS";
export const LOAD_USER_GAMES_FAIL = "LOAD_USER_GAMES_FAIL";

export type LoadUserGamesType = typeof LOAD_USER_GAMES | typeof LOAD_USER_GAMES_SUCCESS | typeof LOAD_USER_GAMES_FAIL;
export type LoadUserGamesActionCreator = AxiosActionCreator<LoadUserGamesType>;
export type LoadUserGamesAction = AxiosAction<LoadUserGamesType>;

export type UserGamesActionCreators = LoadUserGamesActionCreator;
export type UserGamesActions = LoadUserGamesAction;
export type UserGamesState = AxiosState<Game[]>;
