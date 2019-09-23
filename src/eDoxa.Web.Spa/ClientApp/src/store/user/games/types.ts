import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_GAMES = "LOAD_GAMES";
export const LOAD_GAMES_SUCCESS = "LOAD_GAMES_SUCCESS";
export const LOAD_GAMES_FAIL = "LOAD_GAMES_FAIL";

type LoadGamesType = typeof LOAD_GAMES | typeof LOAD_GAMES_SUCCESS | typeof LOAD_GAMES_FAIL;

interface LoadGamesActionCreator extends AxiosActionCreator<LoadGamesType> {}

interface LoadGamesAction extends AxiosAction<LoadGamesType> {}

export type GamesActionCreators = LoadGamesActionCreator;

export type GamesActionTypes = LoadGamesAction;

export interface GamesState {}
