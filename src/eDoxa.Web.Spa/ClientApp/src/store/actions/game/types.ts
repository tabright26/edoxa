import { AxiosActionCreator, AxiosAction } from "utils/axios/types";

export const GENERATE_GAME_AUTHENTICATION = "GENERATE_GAME_AUTHENTICATION";
export const GENERATE_GAME_AUTHENTICATION_SUCCESS =
  "GENERATE_GAME_AUTHENTICATION_SUCCESS";
export const GENERATE_GAME_AUTHENTICATION_FAIL =
  "GENERATE_GAME_AUTHENTICATION_FAIL";

export const VALIDATE_GAME_AUTHENTICATION = "VALIDATE_GAME_AUTHENTICATION";
export const VALIDATE_GAME_AUTHENTICATION_SUCCESS =
  "VALIDATE_GAME_AUTHENTICATION_SUCCESS";
export const VALIDATE_GAME_AUTHENTICATION_FAIL =
  "VALIDATE_GAME_AUTHENTICATION_FAIL";

export type GenerateGameAuthenticationType =
  | typeof GENERATE_GAME_AUTHENTICATION
  | typeof GENERATE_GAME_AUTHENTICATION_SUCCESS
  | typeof GENERATE_GAME_AUTHENTICATION_FAIL;
export type GenerateGameAuthenticationActionCreator = AxiosActionCreator<
  GenerateGameAuthenticationType
>;
export type GenerateGameAuthenticationAction = AxiosAction<
  GenerateGameAuthenticationType
>;

export type ValidateGameAuthenticationType =
  | typeof VALIDATE_GAME_AUTHENTICATION
  | typeof VALIDATE_GAME_AUTHENTICATION_SUCCESS
  | typeof VALIDATE_GAME_AUTHENTICATION_FAIL;
export type ValidateGameAuthenticationActionCreator = AxiosActionCreator<
  ValidateGameAuthenticationType
>;
export type ValidateGameAuthenticationAction = AxiosAction<
  ValidateGameAuthenticationType
>;

export type GameAuthenticationActionCreators =
  | ValidateGameAuthenticationActionCreator
  | GenerateGameAuthenticationActionCreator;
export type GameAuthenticationActions =
  | ValidateGameAuthenticationAction
  | GenerateGameAuthenticationAction;

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
export type UnlinkGameCredentialAction = AxiosAction<UnlinkGameCredentialType>;

export type GameCredentialActionCreators = UnlinkGameCredentialActionCreator;
export type GameCredentialActions = UnlinkGameCredentialAction;
