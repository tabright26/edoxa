import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_BANK_ACCOUNTS = "LOAD_BANK_ACCOUNTS";
export const LOAD_BANK_ACCOUNTS_SUCCESS = "LOAD_BANK_ACCOUNTS_SUCCESS";
export const LOAD_BANK_ACCOUNTS_FAIL = "LOAD_BANK_ACCOUNTS_FAIL";

export const CREATE_BANK_ACCOUNT = "CREATE_BANK_ACCOUNT";
export const CREATE_BANK_ACCOUNT_SUCCESS = "CREATE_BANK_ACCOUNT_SUCCESS";
export const CREATE_BANK_ACCOUNT_FAIL = "CREATE_BANK_ACCOUNT_FAIL";

export const UPDATE_BANK_ACCOUNT = "UPDATE_BANK_ACCOUNT";
export const UPDATE_BANK_ACCOUNT_SUCCESS = "UPDATE_BANK_ACCOUNT_SUCCESS";
export const UPDATE_BANK_ACCOUNT_FAIL = "UPDATE_BANK_ACCOUNT_FAIL";

export const DELETE_BANK_ACCOUNT = "DELETE_BANK_ACCOUNT";
export const DELETE_BANK_ACCOUNT_SUCCESS = "DELETE_BANK_ACCOUNT_SUCCESS";
export const DELETE_BANK_ACCOUNT_FAIL = "DELETE_BANK_ACCOUNT_FAIL";

type LoadBankAccountsType = typeof LOAD_BANK_ACCOUNTS | typeof LOAD_BANK_ACCOUNTS_SUCCESS | typeof LOAD_BANK_ACCOUNTS_FAIL;

interface LoadBankAccountsActionCreator extends AxiosActionCreator<LoadBankAccountsType> {}

interface LoadBankAccountsAction extends AxiosAction<LoadBankAccountsType> {}

type CreateBankAccountType = typeof CREATE_BANK_ACCOUNT | typeof CREATE_BANK_ACCOUNT_SUCCESS | typeof CREATE_BANK_ACCOUNT_FAIL;

interface CreateBankAccountActionCreator extends AxiosActionCreator<CreateBankAccountType> {}

interface CreateBankAccountAction extends AxiosAction<CreateBankAccountType> {}

type UpdateBankAccountType = typeof UPDATE_BANK_ACCOUNT | typeof UPDATE_BANK_ACCOUNT_SUCCESS | typeof UPDATE_BANK_ACCOUNT_FAIL;

interface UpdateBankAccountActionCreator extends AxiosActionCreator<UpdateBankAccountType> {}

interface UpdateBankAccountAction extends AxiosAction<UpdateBankAccountType> {}

type DeleteBankAccountType = typeof DELETE_BANK_ACCOUNT | typeof DELETE_BANK_ACCOUNT_SUCCESS | typeof DELETE_BANK_ACCOUNT_FAIL;

interface DeleteBankAccountActionCreator extends AxiosActionCreator<DeleteBankAccountType> {}

interface DeleteBankAccountAction extends AxiosAction<DeleteBankAccountType> {}

export type BankAccountsActionCreators = LoadBankAccountsActionCreator | CreateBankAccountActionCreator | UpdateBankAccountActionCreator | DeleteBankAccountActionCreator;

export type BankAccountsActionTypes = LoadBankAccountsAction | CreateBankAccountAction | UpdateBankAccountAction | DeleteBankAccountAction;

export interface BankAccountsState {}
