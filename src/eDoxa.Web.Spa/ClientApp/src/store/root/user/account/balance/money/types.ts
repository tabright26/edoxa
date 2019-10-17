import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";
import { Balance } from "types";

export const LOAD_USER_MONEY_ACCOUNT_BALANCE = "LOAD_USER_MONEY_ACCOUNT_BALANCE";
export const LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS = "LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS";
export const LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL = "LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL";

type LoadUserMoneyAccountBalanceType = typeof LOAD_USER_MONEY_ACCOUNT_BALANCE | typeof LOAD_USER_MONEY_ACCOUNT_BALANCE_SUCCESS | typeof LOAD_USER_MONEY_ACCOUNT_BALANCE_FAIL;

export interface LoadUserMoneyAccountBalanceActionCreator extends AxiosActionCreator<LoadUserMoneyAccountBalanceType> {}

interface LoadUserMoneyAccountBalanceAction extends AxiosAction<LoadUserMoneyAccountBalanceType> {}

export type UserMoneyAccountBalanceActions = LoadUserMoneyAccountBalanceAction;

export type UserMoneyAccountBalanceState = AxiosState<Balance>;
