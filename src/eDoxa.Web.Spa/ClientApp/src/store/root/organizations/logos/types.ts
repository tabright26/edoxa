import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";

export const DOWNLOAD_CLAN_LOGO = "DOWNLOAD_CLAN_LOGO";
export const DOWNLOAD_CLAN_LOGO_SUCCESS = "DOWNLOAD_CLAN_LOGO_SUCCESS";
export const DOWNLOAD_CLAN_LOGO_FAIL = "DOWNLOAD_CLAN_LOGO_FAIL";

export const UPLOAD_CLAN_LOGO = "UPLOAD_CLAN_LOGO";
export const UPLOAD_CLAN_LOGO_SUCCESS = "UPLOAD_CLAN_LOGO_SUCCESS";
export const UPLOAD_CLAN_LOGO_FAIL = "UPLOAD_CLAN_LOGO_FAIL";

type DownloadClanLogoType = typeof DOWNLOAD_CLAN_LOGO | typeof DOWNLOAD_CLAN_LOGO_SUCCESS | typeof DOWNLOAD_CLAN_LOGO_FAIL;

interface DownloadClanLogoActionCreator extends AxiosActionCreator<DownloadClanLogoType> {}

interface DownloadClanLogoAction extends AxiosAction<DownloadClanLogoType> {}

type UploadClanLogoType = typeof UPLOAD_CLAN_LOGO | typeof UPLOAD_CLAN_LOGO_SUCCESS | typeof UPLOAD_CLAN_LOGO_FAIL;

interface UploadClanLogoActionCreator extends AxiosActionCreator<UploadClanLogoType> {}

interface UploadClanLogoAction extends AxiosAction<UploadClanLogoType> {}

export type ClanLogosActionCreators = DownloadClanLogoActionCreator | UploadClanLogoActionCreator;

export type ClanLogosActions = DownloadClanLogoAction | UploadClanLogoAction;

export type ClanLogosState = AxiosState;
