import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";
import { Balance } from "types";

export const LOAD_USER_TOKEN_ACCOUNT_BALANCE = "LOAD_USER_TOKEN_ACCOUNT_BALANCE";
export const LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS = "LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS";
export const LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL = "LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL";

type LoadUserTokenAccountBalanceType = typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE | typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS | typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL;

export interface LoadUserTokenAccountBalanceActionCreator extends AxiosActionCreator<LoadUserTokenAccountBalanceType> {}

interface LoadUserTokenAccountBalanceAction extends AxiosAction<LoadUserTokenAccountBalanceType> {}

export type UserTokenAccountBalanceActions = LoadUserTokenAccountBalanceAction;

export type UserTokenAccountBalanceState = AxiosState<Balance>;
