import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_USER_ACCOUNT_BALANCE = "LOAD_USER_ACCOUNT_BALANCE";
export const LOAD_USER_ACCOUNT_BALANCE_SUCCESS = "LOAD_USER_ACCOUNT_BALANCE_SUCCESS";
export const LOAD_USER_ACCOUNT_BALANCE_FAIL = "LOAD_USER_ACCOUNT_BALANCE_FAIL";

type LoadBalanceType = typeof LOAD_USER_ACCOUNT_BALANCE | typeof LOAD_USER_ACCOUNT_BALANCE_SUCCESS | typeof LOAD_USER_ACCOUNT_BALANCE_FAIL;

interface LoadBalanceActionCreator extends AxiosActionCreator<LoadBalanceType> {}

interface LoadBalanceAction extends AxiosAction<LoadBalanceType> {}

export type BalanceActionCreators = LoadBalanceActionCreator;

export type BalanceActionTypes = LoadBalanceAction;

export type BalanceState = AxiosState;
