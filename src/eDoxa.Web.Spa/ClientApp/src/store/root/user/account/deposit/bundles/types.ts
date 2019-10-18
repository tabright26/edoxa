import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";
import { Bundle } from "types";

export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES = "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES";
export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS = "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL = "LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL";

type LoadUserAccountDepositMoneyBundlesType =
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_SUCCESS
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_MONEY_BUNDLES_FAIL;

interface LoadUserAccountDepositMoneyBundlesActionCreator extends AxiosActionCreator<LoadUserAccountDepositMoneyBundlesType> {}

interface LoadUserAccountDepositMoneyBundlesAction extends AxiosAction<LoadUserAccountDepositMoneyBundlesType> {}

export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES = "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES";
export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS = "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL = "LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL";

type LoadUserAccountDepositTokenBundlesType =
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS
  | typeof LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL;

interface LoadUserAccountDepositTokenBundlesActionCreator extends AxiosActionCreator<LoadUserAccountDepositTokenBundlesType> {}

interface LoadUserAccountDepositTokenBundlesAction extends AxiosAction<LoadUserAccountDepositTokenBundlesType> {}

export type UserAccountDepositBundlesActionCreators = LoadUserAccountDepositMoneyBundlesActionCreator | LoadUserAccountDepositTokenBundlesActionCreator;

export type UserAccountDepositBundlesActions = LoadUserAccountDepositMoneyBundlesAction | LoadUserAccountDepositTokenBundlesAction;

export type UserAccountDepositBundlesState = AxiosState<Bundle[]>;
