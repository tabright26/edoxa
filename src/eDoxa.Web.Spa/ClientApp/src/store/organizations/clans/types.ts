import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_CLANS = "LOAD_CLANS";
export const LOAD_CLANS_SUCCESS = "LOAD_CLANS_SUCCESS";
export const LOAD_CLANS_FAIL = "LOAD_CLANS_FAIL";

type LoadClansType = typeof LOAD_CLANS | typeof LOAD_CLANS_SUCCESS | typeof LOAD_CLANS_FAIL;

interface LoadClansActionCreator extends AxiosActionCreator<LoadClansType> {}

interface LoadClansAction extends AxiosAction<LoadClansType> {}

export type ClansActionCreators = LoadClansActionCreator;

export type ClansActionTypes = LoadClansAction;

export interface ClansState {}
