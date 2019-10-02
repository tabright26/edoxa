import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_CLAN = "LOAD_CLAN";
export const LOAD_CLAN_SUCCESS = "LOAD_CLAN_SUCCESS";
export const LOAD_CLAN_FAIL = "LOAD_CLAN_FAIL";

type LoadClanType = typeof LOAD_CLAN | typeof LOAD_CLAN_SUCCESS | typeof LOAD_CLAN_FAIL;

interface LoadClanActionCreator extends AxiosActionCreator<LoadClanType> {}

interface LoadClanAction extends AxiosAction<LoadClanType> {}

export type ClanActionCreators = LoadClanActionCreator;

export type ClanActionTypes = LoadClanAction;

export interface ClanState {}
