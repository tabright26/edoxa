import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_BANK_ACCOUNTS = "LOAD_BANK_ACCOUNTS";
export const LOAD_BANK_ACCOUNTS_SUCCESS = "LOAD_BANK_ACCOUNTS_SUCCESS";
export const LOAD_BANK_ACCOUNTS_FAIL = "LOAD_BANK_ACCOUNTS_FAIL";

export const CHANGE_BANK_ACCOUNT = "CHANGE_BANK_ACCOUNT";
export const CHANGE_BANK_ACCOUNT_SUCCESS = "CHANGE_BANK_ACCOUNT_SUCCESS";
export const CHANGE_BANK_ACCOUNT_FAIL = "CHANGE_BANK_ACCOUNT_FAIL";

type LoadBankAccountsType = typeof LOAD_BANK_ACCOUNTS | typeof LOAD_BANK_ACCOUNTS_SUCCESS | typeof LOAD_BANK_ACCOUNTS_FAIL;

interface LoadBankAccountsActionCreator extends AxiosActionCreator<LoadBankAccountsType> {}

interface LoadBankAccountsAction extends AxiosAction<LoadBankAccountsType> {}

type ChangeBankAccountType = typeof CHANGE_BANK_ACCOUNT | typeof CHANGE_BANK_ACCOUNT_SUCCESS | typeof CHANGE_BANK_ACCOUNT_FAIL;

interface ChangeBankAccountActionCreator extends AxiosActionCreator<ChangeBankAccountType> {}

interface ChangeBankAccountAction extends AxiosAction<ChangeBankAccountType> {}

export type BankAccountsActionCreators = LoadBankAccountsActionCreator | ChangeBankAccountActionCreator;

export type BankAccountsActionTypes = LoadBankAccountsAction | ChangeBankAccountAction;

export interface BankAccountsState {}
