import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Bundle } from "types";

export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES = "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES";
export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS = "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS";
export const LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL = "LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL";

type LoadUserAccountWithdrawalMoneyBundlesType =
  | typeof LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES
  | typeof LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS
  | typeof LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL;

interface LoadUserAccountWithdrawalMoneyBundlesActionCreator extends AxiosActionCreator<LoadUserAccountWithdrawalMoneyBundlesType> {}

interface LoadUserAccountWithdrawalMoneyBundlesAction extends AxiosAction<LoadUserAccountWithdrawalMoneyBundlesType, Bundle[]> {}

export type UserAccountWithdrawalBundlesActionCreators = LoadUserAccountWithdrawalMoneyBundlesActionCreator;

export type UserAccountWithdrawalBundlesActions = LoadUserAccountWithdrawalMoneyBundlesAction;

export type UserAccountWithdrawalBundlesState = AxiosState<Bundle[]>;
