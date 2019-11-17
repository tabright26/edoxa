import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Bundle } from "types";

export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES = "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES";
export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS = "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL = "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL";

export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES = "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES";
export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS = "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL = "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL";

export type LoadUserAccountDepositMoneyBundlesType =
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL;
export type LoadUserAccountDepositMoneyBundlesActionCreator = AxiosActionCreator<LoadUserAccountDepositMoneyBundlesType>;
export type LoadUserAccountDepositMoneyBundlesAction = AxiosAction<LoadUserAccountDepositMoneyBundlesType, Bundle[]>;

export type LoadUserAccountDepositTokenBundlesType =
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL;
export type LoadUserAccountDepositTokenBundlesActionCreator = AxiosActionCreator<LoadUserAccountDepositTokenBundlesType>;
export type LoadUserAccountDepositTokenBundlesAction = AxiosAction<LoadUserAccountDepositTokenBundlesType, Bundle[]>;

export type UserAccountDepositBundlesActionCreators = LoadUserAccountDepositMoneyBundlesActionCreator | LoadUserAccountDepositTokenBundlesActionCreator;
export type UserAccountDepositBundlesActions = LoadUserAccountDepositMoneyBundlesAction | LoadUserAccountDepositTokenBundlesAction;
export type UserAccountDepositBundlesState = AxiosState<Bundle[]>;
