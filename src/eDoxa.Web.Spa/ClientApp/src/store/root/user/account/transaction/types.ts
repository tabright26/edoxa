import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Transaction } from "types";

export const LOAD_USER_ACCOUNT_TRANSACTIONS = "LOAD_USER_ACCOUNT_TRANSACTIONS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS = "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL = "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL";

export type LoadUserAccountTransactionsType = typeof LOAD_USER_ACCOUNT_TRANSACTIONS | typeof LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS | typeof LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL;
export type LoadUserAccountTransactionsActionCreator = AxiosActionCreator<LoadUserAccountTransactionsType>;
export type LoadUserAccountTransactionsAction = AxiosAction<LoadUserAccountTransactionsType, Transaction[]>;

export type UserAccountTransactionsActionCreators = LoadUserAccountTransactionsActionCreator;
export type UserAccountTransactionsActions = LoadUserAccountTransactionsAction;
export type UserAccountTransactionsState = AxiosState<Transaction[]>;
