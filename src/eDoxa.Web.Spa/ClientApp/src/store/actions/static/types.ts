import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import {
  IdentityStaticOptions,
  CashierStaticOptions,
  ChallengesStaticOptions,
  GamesStaticOptions
} from "types";

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

export const LOAD_CASHIER_STATIC_OPTIONS = "LOAD_CASHIER_STATIC_OPTIONS";
export const LOAD_CASHIER_STATIC_OPTIONS_SUCCESS =
  "LOAD_CASHIER_STATIC_OPTIONS_SUCCESS";
export const LOAD_CASHIER_STATIC_OPTIONS_FAIL =
  "LOAD_CASHIER_STATIC_OPTIONS_FAIL";

export type LoadCashierStaticOptionsType =
  | typeof LOAD_CASHIER_STATIC_OPTIONS
  | typeof LOAD_CASHIER_STATIC_OPTIONS_SUCCESS
  | typeof LOAD_CASHIER_STATIC_OPTIONS_FAIL;
export type LoadCashierStaticOptionsActionCreator = AxiosActionCreator<
  LoadCashierStaticOptionsType
>;
export type LoadCashierStaticOptionsAction = AxiosAction<
  LoadCashierStaticOptionsType,
  CashierStaticOptions
>;

export const LOAD_CHALLENGES_STATIC_OPTIONS = "LOAD_CHALLENGES_STATIC_OPTIONS";
export const LOAD_CHALLENGES_STATIC_OPTIONS_SUCCESS =
  "LOAD_CHALLENGES_STATIC_OPTIONS_SUCCESS";
export const LOAD_CHALLENGES_STATIC_OPTIONS_FAIL =
  "LOAD_CHALLENGES_STATIC_OPTIONS_FAIL";

export type LoadChallengesStaticOptionsType =
  | typeof LOAD_CHALLENGES_STATIC_OPTIONS
  | typeof LOAD_CHALLENGES_STATIC_OPTIONS_SUCCESS
  | typeof LOAD_CHALLENGES_STATIC_OPTIONS_FAIL;
export type LoadChallengesStaticOptionsActionCreator = AxiosActionCreator<
  LoadChallengesStaticOptionsType
>;
export type LoadChallengesStaticOptionsAction = AxiosAction<
  LoadChallengesStaticOptionsType,
  ChallengesStaticOptions
>;

export const LOAD_GAMES_STATIC_OPTIONS = "LOAD_GAMES_STATIC_OPTIONS";
export const LOAD_GAMES_STATIC_OPTIONS_SUCCESS =
  "LOAD_GAMES_STATIC_OPTIONS_SUCCESS";
export const LOAD_GAMES_STATIC_OPTIONS_FAIL = "LOAD_GAMES_STATIC_OPTIONS_FAIL";

export type LoadGamesStaticOptionsType =
  | typeof LOAD_GAMES_STATIC_OPTIONS
  | typeof LOAD_GAMES_STATIC_OPTIONS_SUCCESS
  | typeof LOAD_GAMES_STATIC_OPTIONS_FAIL;
export type LoadGamesStaticOptionsActionCreator = AxiosActionCreator<
  LoadGamesStaticOptionsType
>;
export type LoadGamesStaticOptionsAction = AxiosAction<
  LoadGamesStaticOptionsType,
  GamesStaticOptions
>;

export type StaticOptionsActionCreators =
  | LoadGamesStaticOptionsActionCreator
  | LoadChallengesStaticOptionsActionCreator
  | LoadIdentityStaticOptionsActionCreator
  | LoadCashierStaticOptionsActionCreator;

export type StaticOptionsActions =
  | LoadGamesStaticOptionsAction
  | LoadChallengesStaticOptionsAction
  | LoadIdentityStaticOptionsAction
  | LoadCashierStaticOptionsAction;
