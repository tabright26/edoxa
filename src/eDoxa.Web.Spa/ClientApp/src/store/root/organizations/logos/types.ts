import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";

export const DOWNLOAD_CLAN_LOGO = "DOWNLOAD_CLAN_LOGO";
export const DOWNLOAD_CLAN_LOGO_SUCCESS = "DOWNLOAD_CLAN_LOGO_SUCCESS";
export const DOWNLOAD_CLAN_LOGO_FAIL = "DOWNLOAD_CLAN_LOGO_FAIL";

export const UPLOAD_CLAN_LOGO = "UPLOAD_CLAN_LOGO";
export const UPLOAD_CLAN_LOGO_SUCCESS = "UPLOAD_CLAN_LOGO_SUCCESS";
export const UPLOAD_CLAN_LOGO_FAIL = "UPLOAD_CLAN_LOGO_FAIL";

export type DownloadClanLogoType = typeof DOWNLOAD_CLAN_LOGO | typeof DOWNLOAD_CLAN_LOGO_SUCCESS | typeof DOWNLOAD_CLAN_LOGO_FAIL;
export type DownloadClanLogoActionCreator = AxiosActionCreator<DownloadClanLogoType>;
export type DownloadClanLogoAction = AxiosAction<DownloadClanLogoType>;

export type UploadClanLogoType = typeof UPLOAD_CLAN_LOGO | typeof UPLOAD_CLAN_LOGO_SUCCESS | typeof UPLOAD_CLAN_LOGO_FAIL;
export type UploadClanLogoActionCreator = AxiosActionCreator<UploadClanLogoType>;
export type UploadClanLogoAction = AxiosAction<UploadClanLogoType>;

export type ClanLogosActionCreators = DownloadClanLogoActionCreator | UploadClanLogoActionCreator;
export type ClanLogosActions = DownloadClanLogoAction | UploadClanLogoAction;
export type ClanLogosState = AxiosState;
