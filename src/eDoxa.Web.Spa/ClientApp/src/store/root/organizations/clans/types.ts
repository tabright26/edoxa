import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";

export const LOAD_CLANS = "LOAD_CLANS";
export const LOAD_CLANS_SUCCESS = "LOAD_CLANS_SUCCESS";
export const LOAD_CLANS_FAIL = "LOAD_CLANS_FAIL";

export const LOAD_CLAN = "LOAD_CLAN";
export const LOAD_CLAN_SUCCESS = "LOAD_CLAN_SUCCESS";
export const LOAD_CLAN_FAIL = "LOAD_CLAN_FAIL";

export const CREATE_CLAN = "CREATE_CLAN";
export const CREATE_CLAN_SUCCESS = "CREATE_CLAN_SUCCESS";
export const CREATE_CLAN_FAIL = "CREATE_CLAN_FAIL";

export const LEAVE_CLAN = "LEAVE_CLAN";
export const LEAVE_CLAN_SUCCESS = "LEAVE_CLAN_SUCCESS";
export const LEAVE_CLAN_FAIL = "LEAVE_CLAN_FAIL";

export type LoadClansType = typeof LOAD_CLANS | typeof LOAD_CLANS_SUCCESS | typeof LOAD_CLANS_FAIL;
export type LoadClansActionCreator = AxiosActionCreator<LoadClansType>;
export type LoadClansAction = AxiosAction<LoadClansType>;

export type LoadClanType = typeof LOAD_CLAN | typeof LOAD_CLAN_SUCCESS | typeof LOAD_CLAN_FAIL;
export type LoadClanActionCreator = AxiosActionCreator<LoadClanType>;
export type LoadClanAction = AxiosAction<LoadClanType>;

export type CreateClanType = typeof CREATE_CLAN | typeof CREATE_CLAN_SUCCESS | typeof CREATE_CLAN_FAIL;
export type CreateClanActionCreator = AxiosActionCreator<CreateClanType>;
export type CreateClanAction = AxiosAction<CreateClanType>;

export type LeaveClanType = typeof LEAVE_CLAN | typeof LEAVE_CLAN_SUCCESS | typeof LEAVE_CLAN_FAIL;
export type LeaveClanActionCreator = AxiosActionCreator<LeaveClanType>;
export type LeaveClanAction = AxiosAction<LeaveClanType>;

export type ClansActionCreators = LoadClansActionCreator | LoadClanActionCreator | CreateClanActionCreator | LeaveClanActionCreator;
export type ClansActions = LoadClansAction | LoadClanAction | CreateClanAction | LeaveClanAction;
export type ClansState = AxiosState;
