import { AxiosActionCreator, AxiosAction } from "store/types";

export const LOAD_BANK_ACCOUNT = "LOAD_BANK_ACCOUNT";
export const LOAD_BANK_ACCOUNT_SUCCESS = "LOAD_BANK_ACCOUNT_SUCCESS";
export const LOAD_BANK_ACCOUNT_FAIL = "LOAD_BANK_ACCOUNT_FAIL";

export const UPDATE_BANK_ACCOUNT = "UPDATE_BANK_ACCOUNT";
export const UPDATE_BANK_ACCOUNT_SUCCESS = "UPDATE_BANK_ACCOUNT_SUCCESS";
export const UPDATE_BANK_ACCOUNT_FAIL = "UPDATE_BANK_ACCOUNT_FAIL";

type LoadBankAccountType = typeof LOAD_BANK_ACCOUNT | typeof LOAD_BANK_ACCOUNT_SUCCESS | typeof LOAD_BANK_ACCOUNT_FAIL;

interface LoadBankAccountActionCreator extends AxiosActionCreator<LoadBankAccountType> {}

interface LoadBankAccountAction extends AxiosAction<LoadBankAccountType> {}

type UpdateBankAccountType = typeof UPDATE_BANK_ACCOUNT | typeof UPDATE_BANK_ACCOUNT_SUCCESS | typeof UPDATE_BANK_ACCOUNT_FAIL;

interface UpdateBankAccountActionCreator extends AxiosActionCreator<UpdateBankAccountType> {}

interface UpdateBankAccountAction extends AxiosAction<UpdateBankAccountType> {}

export type BankAccountActionCreators = LoadBankAccountActionCreator | UpdateBankAccountActionCreator;

export type BankAccountActionTypes = LoadBankAccountAction | UpdateBankAccountAction;

export interface BankAccountState {}
