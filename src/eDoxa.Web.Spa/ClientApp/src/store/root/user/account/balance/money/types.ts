import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import { Balance } from "types";

export const LOAD_USER_MONEY_ACCOUNT_BALANCE = "LOAD_USER_MONEY_ACCOUNT_BALANCE";
export const LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS = "LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS";
export const LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL = "LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL";

export type LoadUserMoneyAccountBalanceType = typeof LOAD_USER_MONEY_ACCOUNT_BALANCE | typeof LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS | typeof LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL;
export type LoadUserMoneyAccountBalanceActionCreator = AxiosActionCreator<LoadUserMoneyAccountBalanceType>;
export type LoadUserMoneyAccountBalanceAction = AxiosAction<LoadUserMoneyAccountBalanceType, Balance>;

export type UserMoneyAccountBalanceActions = LoadUserMoneyAccountBalanceAction;
