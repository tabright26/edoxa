import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_CLANS = "LOAD_CLANS";
export const LOAD_CLANS_SUCCESS = "LOAD_CLANS_SUCCESS";
export const LOAD_CLANS_FAIL = "LOAD_CLANS_FAIL";

export const LOAD_CLAN = "LOAD_CLAN";
export const LOAD_CLAN_SUCCESS = "LOAD_CLAN_SUCCESS";
export const LOAD_CLAN_FAIL = "LOAD_CLAN_FAIL";

export const ADD_CLAN = "ADD_CLAN";
export const ADD_CLAN_SUCCESS = "ADD_CLAN_SUCCESS";
export const ADD_CLAN_FAIL = "ADD_CLAN_FAIL";

export const DOWNLOAD_CLAN_LOGO = "DOWNLOAD_CLAN_LOGO";
export const DOWNLOAD_CLAN_LOGO_SUCCESS = "DOWNLOAD_CLAN_LOGO_SUCCESS";
export const DOWNLOAD_CLAN_LOGO_FAIL = "DOWNLOAD_CLAN_LOGO_FAIL";

export const UPLOAD_CLAN_LOGO = "UPLOAD_CLAN_LOGO";
export const UPLOAD_CLAN_LOGO_SUCCESS = "UPLOAD_CLAN_LOGO_SUCCESS";
export const UPLOAD_CLAN_LOGO_FAIL = "UPLOAD_CLAN_LOGO_FAIL";

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

type DownloadClanLogoType = typeof DOWNLOAD_CLAN_LOGO | typeof DOWNLOAD_CLAN_LOGO_SUCCESS | typeof DOWNLOAD_CLAN_LOGO_FAIL;

interface DownloadClanLogoActionCreator extends AxiosActionCreator<DownloadClanLogoType> {}

interface DownloadClanLogoAction extends AxiosAction<DownloadClanLogoType> {}

//----------------------------------------------------------------------------------------------

type UploadClanLogoType = typeof UPLOAD_CLAN_LOGO | typeof UPLOAD_CLAN_LOGO_SUCCESS | typeof UPLOAD_CLAN_LOGO_FAIL;

interface UploadClanLogoActionCreator extends AxiosActionCreator<UploadClanLogoType> {}

interface UploadClanLogoAction extends AxiosAction<UploadClanLogoType> {}

//----------------------------------------------------------------------------------------------

export type ClansActionCreators = LoadClansActionCreator | LoadClanActionCreator | AddClanActionCreator | DownloadClanLogoActionCreator | UploadClanLogoActionCreator;

export type ClansActionTypes = LoadClansAction | LoadClanAction | AddClanAction | DownloadClanLogoAction | UploadClanLogoAction;

export type ClansState = AxiosState;
