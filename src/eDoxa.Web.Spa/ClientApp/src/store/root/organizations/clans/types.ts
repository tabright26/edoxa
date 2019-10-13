import { AxiosActionCreator, AxiosAction } from "store/middlewares/axios/types";

export const LOAD_CLANS = "LOAD_CLANS";
export const LOAD_CLANS_SUCCESS = "LOAD_CLANS_SUCCESS";
export const LOAD_CLANS_FAIL = "LOAD_CLANS_FAIL";

export const LOAD_CLAN = "LOAD_CLAN";
export const LOAD_CLAN_SUCCESS = "LOAD_CLAN_SUCCESS";
export const LOAD_CLAN_FAIL = "LOAD_CLAN_FAIL";

export const ADD_CLAN = "ADD_CLAN";
export const ADD_CLAN_SUCCESS = "ADD_CLAN_SUCCESS";
export const ADD_CLAN_FAIL = "ADD_CLAN_FAIL";

export const LOAD_LOGO = "LOAD_LOGO";
export const LOAD_LOGO_SUCCESS = "LOAD_LOGO_SUCCESS";
export const LOAD_LOGO_FAIL = "LOAD_LOGO_FAIL";

export const UPDATE_LOGO = "UPDATE_LOGO";
export const UPDATE_LOGO_SUCCESS = "UPDATE_LOGO_SUCCESS";
export const UPDATE_LOGO_FAIL = "UPDATE_LOGO_FAIL";

type LoadClansType = typeof LOAD_CLANS | typeof LOAD_CLANS_SUCCESS | typeof LOAD_CLANS_FAIL;

interface LoadClansActionCreator extends AxiosActionCreator<LoadClansType> {}

interface LoadClansAction extends AxiosAction<LoadClansType> {}

//------------------------------------------------------------------------------------------------

type LoadClanType = typeof LOAD_CLAN | typeof LOAD_CLAN_SUCCESS | typeof LOAD_CLAN_FAIL;

interface LoadClanActionCreator extends AxiosActionCreator<LoadClanType> {}

interface LoadClanAction extends AxiosAction<LoadClanType> {}

//------------------------------------------------------------------------------------------------

type AddClanType = typeof ADD_CLAN | typeof ADD_CLAN_SUCCESS | typeof ADD_CLAN_FAIL;

interface AddClanActionCreator extends AxiosActionCreator<AddClanType> {}

interface AddClanAction extends AxiosAction<AddClanType> {}

//------------------------------------------------------------------------------------------------

type LoadLogoType = typeof LOAD_LOGO | typeof LOAD_LOGO_SUCCESS | typeof LOAD_LOGO_FAIL;

interface LoadLogoActionCreator extends AxiosActionCreator<LoadLogoType> {}

interface LoadLogoAction extends AxiosAction<LoadLogoType> {}

//----------------------------------------------------------------------------------------------

type UpdateLogoType = typeof UPDATE_LOGO | typeof UPDATE_LOGO_SUCCESS | typeof UPDATE_LOGO_FAIL;

interface UpdateLogoActionCreator extends AxiosActionCreator<UpdateLogoType> {}

interface UpdateLogoAction extends AxiosAction<UpdateLogoType> {}

//----------------------------------------------------------------------------------------------

export type ClansActionCreators = LoadClansActionCreator | LoadClanActionCreator | AddClanActionCreator | LoadLogoActionCreator | UpdateLogoActionCreator;

export type ClansActionTypes = LoadClansAction | LoadClanAction | AddClanAction | LoadLogoAction | UpdateLogoAction;

export interface ClansState {}
