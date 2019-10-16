import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_STRIPE_ACCOUNT = "LOAD_STRIPE_ACCOUNT";
export const LOAD_STRIPE_ACCOUNT_SUCCESS = "LOAD_STRIPE_ACCOUNT_SUCCESS";
export const LOAD_STRIPE_ACCOUNT_FAIL = "LOAD_STRIPE_ACCOUNT_FAIL";

type LoadStripeAccountType = typeof LOAD_STRIPE_ACCOUNT | typeof LOAD_STRIPE_ACCOUNT_SUCCESS | typeof LOAD_STRIPE_ACCOUNT_FAIL;

interface LoadStripeAccountActionCreator extends AxiosActionCreator<LoadStripeAccountType> {}

interface LoadStripeAccountAction extends AxiosAction<LoadStripeAccountType> {}

export type StripeAccountActionCreators = LoadStripeAccountActionCreator;

export type StripeAccountActions = LoadStripeAccountAction;

export type StripeAccountState = AxiosState;
