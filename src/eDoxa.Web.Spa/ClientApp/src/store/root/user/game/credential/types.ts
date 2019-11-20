import { AxiosActionCreator, AxiosAction } from "utils/axios/types";

export const UNLINK_GAME_CREDENTIAL = "UNLINK_GAME_CREDENTIAL";
export const UNLINK_GAME_CREDENTIAL_SUCCESS = "UNLINK_GAME_CREDENTIAL_SUCCESS";
export const UNLINK_GAME_CREDENTIAL_FAIL = "UNLINK_GAME_CREDENTIAL_FAIL";

export type UnlinkGameCredentialType =
  | typeof UNLINK_GAME_CREDENTIAL
  | typeof UNLINK_GAME_CREDENTIAL_SUCCESS
  | typeof UNLINK_GAME_CREDENTIAL_FAIL;
export type UnlinkGameCredentialActionCreator = AxiosActionCreator<
  UnlinkGameCredentialType
>;
export type UnlinkGameCredentialAction = AxiosAction<
  UnlinkGameCredentialType
>;

export type GameCredentialActionCreators = UnlinkGameCredentialActionCreator;
export type GameCredentialActions = UnlinkGameCredentialAction;
