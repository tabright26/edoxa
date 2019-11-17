import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { StripeAccount } from "types";

export const LOAD_STRIPE_ACCOUNT = "LOAD_STRIPE_ACCOUNT";
export const LOAD_STRIPE_ACCOUNT_SUCCESS = "LOAD_STRIPE_ACCOUNT_SUCCESS";
export const LOAD_STRIPE_ACCOUNT_FAIL = "LOAD_STRIPE_ACCOUNT_FAIL";

export type LoadStripeAccountType = typeof LOAD_STRIPE_ACCOUNT | typeof LOAD_STRIPE_ACCOUNT_SUCCESS | typeof LOAD_STRIPE_ACCOUNT_FAIL;

export type LoadStripeAccountActionCreator = AxiosActionCreator<LoadStripeAccountType>;

export type LoadStripeAccountAction = AxiosAction<LoadStripeAccountType, StripeAccount>;

export type StripeAccountActionCreators = LoadStripeAccountActionCreator;

export type StripeAccountActions = LoadStripeAccountAction;

export type StripeAccountState = AxiosState<StripeAccount>;
