import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_ACCOUNT = "LOAD_ACCOUNT";
export const LOAD_ACCOUNT_SUCCESS = "LOAD_ACCOUNT_SUCCESS";
export const LOAD_ACCOUNT_FAIL = "LOAD_ACCOUNT_FAIL";

type LoadAccountType = typeof LOAD_ACCOUNT | typeof LOAD_ACCOUNT_SUCCESS | typeof LOAD_ACCOUNT_FAIL;

interface LoadAccountActionCreator extends AxiosActionCreator<LoadAccountType> {}

interface LoadAccountAction extends AxiosAction<LoadAccountType> {}

export type AccountActionCreators = LoadAccountActionCreator;

export type AccountActionTypes = LoadAccountAction;

export type AccountState = AxiosState;
