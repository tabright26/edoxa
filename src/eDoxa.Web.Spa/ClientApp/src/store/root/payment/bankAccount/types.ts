import { AxiosActionCreator, AxiosAction } from "store/middlewares/axios/types";

export const LOAD_BANK_ACCOUNT = "LOAD_BANK_ACCOUNT";
export const LOAD_BANK_ACCOUNT_SUCCESS = "LOAD_BANK_ACCOUNT_SUCCESS";
export const LOAD_BANK_ACCOUNT_FAIL = "LOAD_BANK_ACCOUNT_FAIL";

export const CHANGE_BANK_ACCOUNT = "CHANGE_BANK_ACCOUNT";
export const CHANGE_BANK_ACCOUNT_SUCCESS = "CHANGE_BANK_ACCOUNT_SUCCESS";
export const CHANGE_BANK_ACCOUNT_FAIL = "CHANGE_BANK_ACCOUNT_FAIL";

type LoadBankAccountType = typeof LOAD_BANK_ACCOUNT | typeof LOAD_BANK_ACCOUNT_SUCCESS | typeof LOAD_BANK_ACCOUNT_FAIL;

interface LoadBankAccountActionCreator extends AxiosActionCreator<LoadBankAccountType> {}

interface LoadBankAccountAction extends AxiosAction<LoadBankAccountType> {}

type ChangeBankAccountType = typeof CHANGE_BANK_ACCOUNT | typeof CHANGE_BANK_ACCOUNT_SUCCESS | typeof CHANGE_BANK_ACCOUNT_FAIL;

interface ChangeBankAccountActionCreator extends AxiosActionCreator<ChangeBankAccountType> {}

interface ChangeBankAccountAction extends AxiosAction<ChangeBankAccountType> {}

export type BankAccountActionCreators = LoadBankAccountActionCreator | ChangeBankAccountActionCreator;

export type BankAccountActionTypes = LoadBankAccountAction | ChangeBankAccountAction;

export interface BankAccountState {}
