import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_USER_ACCOUNT_TRANSACTIONS = "LOAD_USER_ACCOUNT_TRANSACTIONS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS = "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL = "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL";

type LoadUserAccountTransactionsType = typeof LOAD_USER_ACCOUNT_TRANSACTIONS | typeof LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS | typeof LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL;

interface LoadUserAccountTransactionsActionCreator extends AxiosActionCreator<LoadUserAccountTransactionsType> {}

interface LoadUserAccountTransactionsAction extends AxiosAction<LoadUserAccountTransactionsType> {}

export type UserAccountTransactionsActionCreators = LoadUserAccountTransactionsActionCreator;

export type UserAccountTransactionsActions = LoadUserAccountTransactionsAction;

export type UserAccountTransactionsState = AxiosState;
