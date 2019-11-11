import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { GameCredential } from "types";

export const LOAD_GAME_CREDENTIAL = "LOAD_GAME_CREDENTIAL";
export const LOAD_GAME_CREDENTIAL_SUCCESS = "LOAD_GAME_CREDENTIAL_SUCCESS";
export const LOAD_GAME_CREDENTIAL_FAIL = "LOAD_GAME_CREDENTIAL_FAIL";

export const LINK_GAME_CREDENTIAL = "LINK_GAME_CREDENTIAL";
export const LINK_GAME_CREDENTIAL_SUCCESS = "LINK_GAME_CREDENTIAL_SUCCESS";
export const LINK_GAME_CREDENTIAL_FAIL = "LINK_GAME_CREDENTIAL_FAIL";

export const UNLINK_GAME_CREDENTIAL = "UNLINK_GAME_CREDENTIAL";
export const UNLINK_GAME_CREDENTIAL_SUCCESS = "UNLINK_GAME_CREDENTIAL_SUCCESS";
export const UNLINK_GAME_CREDENTIAL_FAIL = "UNLINK_GAME_CREDENTIAL_FAIL";

export type LoadGameCredentialType = typeof LOAD_GAME_CREDENTIAL | typeof LOAD_GAME_CREDENTIAL_SUCCESS | typeof LOAD_GAME_CREDENTIAL_FAIL;
export type LoadGameCredentialActionCreator = AxiosActionCreator<LoadGameCredentialType>;
export type LoadGameCredentialAction = AxiosAction<LoadGameCredentialType>;

export type LinkGameCredentialType = typeof LINK_GAME_CREDENTIAL | typeof LINK_GAME_CREDENTIAL_SUCCESS | typeof LINK_GAME_CREDENTIAL_FAIL;
export type LinkGameCredentialActionCreator = AxiosActionCreator<LinkGameCredentialType>;
export type LinkGameCredentialAction = AxiosAction<LinkGameCredentialType>;

export type UnlinkGameCredentialType = typeof UNLINK_GAME_CREDENTIAL | typeof UNLINK_GAME_CREDENTIAL_SUCCESS | typeof UNLINK_GAME_CREDENTIAL_FAIL;
export type UnlinkGameCredentialActionCreator = AxiosActionCreator<UnlinkGameCredentialType>;
export type UnlinkGameCredentialAction = AxiosAction<UnlinkGameCredentialType>;

export type GameCredentialsActionCreators = LoadGameCredentialActionCreator | LinkGameCredentialActionCreator | UnlinkGameCredentialActionCreator;
export type GameCredentialsActions = LoadGameCredentialAction | LinkGameCredentialAction | UnlinkGameCredentialAction;
export type GameCredentialsState = AxiosState<GameCredential[]>;
