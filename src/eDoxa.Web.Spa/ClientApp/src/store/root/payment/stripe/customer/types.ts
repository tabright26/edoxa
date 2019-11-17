import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { StripeCustomer } from "types";

export const LOAD_STRIPE_CUSTOMER = "LOAD_STRIPE_CUSTOMER";
export const LOAD_STRIPE_CUSTOMER_SUCCESS = "LOAD_STRIPE_CUSTOMER_SUCCESS";
export const LOAD_STRIPE_CUSTOMER_FAIL = "LOAD_STRIPE_CUSTOMER_FAIL";

export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD = "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD";
export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS = "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS";
export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL = "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL";

export type LoadStripeCustomerType = typeof LOAD_STRIPE_CUSTOMER | typeof LOAD_STRIPE_CUSTOMER_SUCCESS | typeof LOAD_STRIPE_CUSTOMER_FAIL;

export type LoadStripeCustomerActionCreator = AxiosActionCreator<LoadStripeCustomerType>;

export type LoadStripeCustomerAction = AxiosAction<LoadStripeCustomerType, StripeCustomer>;

export type UpdateStripeCustomerDefaultPaymentMethodType =
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL;

export type UpdateStripeCustomerDefaultPaymentMethodActionCreator = AxiosActionCreator<UpdateStripeCustomerDefaultPaymentMethodType>;

export type UpdateStripeCustomerDefaultPaymentMethodAction = AxiosAction<UpdateStripeCustomerDefaultPaymentMethodType, StripeCustomer>;

export type StripeCustomerActionCreators = LoadStripeCustomerActionCreator | UpdateStripeCustomerDefaultPaymentMethodActionCreator;

export type StripeCustomerActions = LoadStripeCustomerAction | UpdateStripeCustomerDefaultPaymentMethodAction;

export type StripeCustomerState = AxiosState<StripeCustomer>;
