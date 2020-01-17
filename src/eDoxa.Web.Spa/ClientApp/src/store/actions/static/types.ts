import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import { IdentityStaticOptions } from "types";

export const LOAD_IDENTITY_STATIC_OPTIONS = "LOAD_IDENTITY_STATIC_OPTIONS";
export const LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS =
  "LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS";
export const LOAD_IDENTITY_STATIC_OPTIONS_FAIL =
  "LOAD_IDENTITY_STATIC_OPTIONS_FAIL";

export type LoadIdentityStaticOptionsType =
  | typeof LOAD_IDENTITY_STATIC_OPTIONS
  | typeof LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS
  | typeof LOAD_IDENTITY_STATIC_OPTIONS_FAIL;

export type LoadIdentityStaticOptionsActionCreator = AxiosActionCreator<
  LoadIdentityStaticOptionsType
>;

export type LoadIdentityStaticOptionsAction = AxiosAction<
  LoadIdentityStaticOptionsType,
  IdentityStaticOptions
>;

export type IdentityStaticOptionsActionCreators = LoadIdentityStaticOptionsActionCreator;

export type IdentityStaticOptionsActions = LoadIdentityStaticOptionsAction;
