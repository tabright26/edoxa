import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_USER_ACCOUNT_TRANSACTIONS = "LOAD_USER_ACCOUNT_TRANSACTIONS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS = "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL = "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL";

type LoadTransactionsType = typeof LOAD_USER_ACCOUNT_TRANSACTIONS | typeof LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS | typeof LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL;

interface LoadTransactionsActionCreator extends AxiosActionCreator<LoadTransactionsType> {}

interface LoadTransactionsAction extends AxiosAction<LoadTransactionsType> {}

export type TransactionsActionCreators = LoadTransactionsActionCreator;

export type TransactionsActionTypes = LoadTransactionsAction;

export interface TransactionsState {}
