import { AxiosActionCreator, AxiosAction } from "store/types";

export const LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME = "LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME";
export const LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS = "LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS";
export const LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL = "LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL";

type LoadLeagueOfLegendsSummonersByNameType =
  | typeof LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME
  | typeof LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS
  | typeof LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL;

interface LoadLeagueOfLegendsSummonersByNameActionCreator extends AxiosActionCreator<LoadLeagueOfLegendsSummonersByNameType> {}

interface LoadLeagueOfLegendsSummonersByNameAction extends AxiosAction<LoadLeagueOfLegendsSummonersByNameType> {}

export type LeagueOfLegendsActionCreators = LoadLeagueOfLegendsSummonersByNameActionCreator;

export type LeagueOfLegendsActionTypes = LoadLeagueOfLegendsSummonersByNameAction;

export interface LeagueOfLegendsState {}
