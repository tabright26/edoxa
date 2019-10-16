import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

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

type LoadClansType = typeof LOAD_CLANS | typeof LOAD_CLANS_SUCCESS | typeof LOAD_CLANS_FAIL;

interface LoadClansActionCreator extends AxiosActionCreator<LoadClansType> {}

interface LoadClansAction extends AxiosAction<LoadClansType> {}

type LoadClanType = typeof LOAD_CLAN | typeof LOAD_CLAN_SUCCESS | typeof LOAD_CLAN_FAIL;

interface LoadClanActionCreator extends AxiosActionCreator<LoadClanType> {}

interface LoadClanAction extends AxiosAction<LoadClanType> {}

type CreateClanType = typeof CREATE_CLAN | typeof CREATE_CLAN_SUCCESS | typeof CREATE_CLAN_FAIL;

interface CreateClanActionCreator extends AxiosActionCreator<CreateClanType> {}

interface CreateClanAction extends AxiosAction<CreateClanType> {}

type LeaveClanType = typeof LEAVE_CLAN | typeof LEAVE_CLAN_SUCCESS | typeof LEAVE_CLAN_FAIL;

interface LeaveClanActionCreator extends AxiosActionCreator<LeaveClanType> {}

interface LeaveClanAction extends AxiosAction<LeaveClanType> {}

export type ClansActionCreators = LoadClansActionCreator | LoadClanActionCreator | CreateClanActionCreator | LeaveClanActionCreator;

export type ClansActions = LoadClansAction | LoadClanAction | CreateClanAction | LeaveClanAction;

export type ClansState = AxiosState;
