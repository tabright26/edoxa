import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { StripeBankAccount } from "types";

export const LOAD_STRIPE_BANKACCOUNT = "LOAD_STRIPE_BANKACCOUNT";
export const LOAD_STRIPE_BANKACCOUNT_SUCCESS = "LOAD_STRIPE_BANKACCOUNT_SUCCESS";
export const LOAD_STRIPE_BANKACCOUNT_FAIL = "LOAD_STRIPE_BANKACCOUNT_FAIL";

export const UPDATE_STRIPE_BANKACCOUNT = "UPDATE_STRIPE_BANKACCOUNT";
export const UPDATE_STRIPE_BANKACCOUNT_SUCCESS = "UPDATE_STRIPE_BANKACCOUNT_SUCCESS";
export const UPDATE_STRIPE_BANKACCOUNT_FAIL = "UPDATE_STRIPE_BANKACCOUNT_FAIL";

export type LoadStripeBankAccountType = typeof LOAD_STRIPE_BANKACCOUNT | typeof LOAD_STRIPE_BANKACCOUNT_SUCCESS | typeof LOAD_STRIPE_BANKACCOUNT_FAIL;

export type LoadStripeBankAccountActionCreator = AxiosActionCreator<LoadStripeBankAccountType>;

export type LoadStripeBankAccountAction = AxiosAction<LoadStripeBankAccountType, StripeBankAccount>;

export type UpdateStripeBankAccountType = typeof UPDATE_STRIPE_BANKACCOUNT | typeof UPDATE_STRIPE_BANKACCOUNT_SUCCESS | typeof UPDATE_STRIPE_BANKACCOUNT_FAIL;

export type UpdateStripeBankAccountActionCreator = AxiosActionCreator<UpdateStripeBankAccountType>;

export type UpdateStripeBankAccountAction = AxiosAction<UpdateStripeBankAccountType, StripeBankAccount>;

export type StripeBankAccountActionCreators = LoadStripeBankAccountActionCreator | UpdateStripeBankAccountActionCreator;

export type StripeBankAccountActions = LoadStripeBankAccountAction | UpdateStripeBankAccountAction;

export type StripeBankAccountState = AxiosState<StripeBankAccount>;
