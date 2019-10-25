import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import { Balance } from "types";

export const LOAD_USER_TOKEN_ACCOUNT_BALANCE = "LOAD_USER_TOKEN_ACCOUNT_BALANCE";
export const LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS = "LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS";
export const LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL = "LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL";

export type LoadUserTokenAccountBalanceType = typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE | typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS | typeof LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL;
export type LoadUserTokenAccountBalanceActionCreator = AxiosActionCreator<LoadUserTokenAccountBalanceType>;
export type LoadUserTokenAccountBalanceAction = AxiosAction<LoadUserTokenAccountBalanceType, Balance>;

export type UserTokenAccountBalanceActions = LoadUserTokenAccountBalanceAction;
