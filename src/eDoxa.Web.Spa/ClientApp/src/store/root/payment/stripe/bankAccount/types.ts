import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";

export const LOAD_STRIPE_BANKACCOUNT = "LOAD_STRIPE_BANKACCOUNT";
export const LOAD_STRIPE_BANKACCOUNT_SUCCESS = "LOAD_STRIPE_BANKACCOUNT_SUCCESS";
export const LOAD_STRIPE_BANKACCOUNT_FAIL = "LOAD_STRIPE_BANKACCOUNT_FAIL";

export const UPDATE_STRIPE_BANKACCOUNT = "UPDATE_STRIPE_BANKACCOUNT";
export const UPDATE_STRIPE_BANKACCOUNT_SUCCESS = "UPDATE_STRIPE_BANKACCOUNT_SUCCESS";
export const UPDATE_STRIPE_BANKACCOUNT_FAIL = "UPDATE_STRIPE_BANKACCOUNT_FAIL";

type LoadStripeBankAccountType = typeof LOAD_STRIPE_BANKACCOUNT | typeof LOAD_STRIPE_BANKACCOUNT_SUCCESS | typeof LOAD_STRIPE_BANKACCOUNT_FAIL;

interface LoadStripeBankAccountActionCreator extends AxiosActionCreator<LoadStripeBankAccountType> {}

interface LoadStripeBankAccountAction extends AxiosAction<LoadStripeBankAccountType> {}

type UpdateStripeBankAccountType = typeof UPDATE_STRIPE_BANKACCOUNT | typeof UPDATE_STRIPE_BANKACCOUNT_SUCCESS | typeof UPDATE_STRIPE_BANKACCOUNT_FAIL;

interface UpdateStripeBankAccountActionCreator extends AxiosActionCreator<UpdateStripeBankAccountType> {}

interface UpdateStripeBankAccountAction extends AxiosAction<UpdateStripeBankAccountType> {}

export type StripeBankAccountActionCreators = LoadStripeBankAccountActionCreator | UpdateStripeBankAccountActionCreator;

export type StripeBankAccountActions = LoadStripeBankAccountAction | UpdateStripeBankAccountAction;

export type StripeBankAccountState = AxiosState;
