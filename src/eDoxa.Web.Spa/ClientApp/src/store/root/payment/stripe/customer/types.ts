import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import * as Stripe from "stripe";

export const LOAD_STRIPE_CUSTOMER = "LOAD_STRIPE_CUSTOMER";
export const LOAD_STRIPE_CUSTOMER_SUCCESS = "LOAD_STRIPE_CUSTOMER_SUCCESS";
export const LOAD_STRIPE_CUSTOMER_FAIL = "LOAD_STRIPE_CUSTOMER_FAIL";

export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD = "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD";
export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS = "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS";
export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL = "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL";

type LoadStripeCustomerType = typeof LOAD_STRIPE_CUSTOMER | typeof LOAD_STRIPE_CUSTOMER_SUCCESS | typeof LOAD_STRIPE_CUSTOMER_FAIL;

interface LoadStripeCustomerActionCreator extends AxiosActionCreator<LoadStripeCustomerType> {}

interface LoadStripeCustomerAction extends AxiosAction<LoadStripeCustomerType, Stripe.customers.ICustomer> {}

type UpdateStripeCustomerDefaultPaymentMethodType =
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL;

interface UpdateStripeCustomerDefaultPaymentMethodActionCreator extends AxiosActionCreator<UpdateStripeCustomerDefaultPaymentMethodType> {}

interface UpdateStripeCustomerDefaultPaymentMethodAction extends AxiosAction<UpdateStripeCustomerDefaultPaymentMethodType, Stripe.customers.ICustomer> {}

export type StripeCustomerActionCreators = LoadStripeCustomerActionCreator | UpdateStripeCustomerDefaultPaymentMethodActionCreator;

export type StripeCustomerActions = LoadStripeCustomerAction | UpdateStripeCustomerDefaultPaymentMethodAction;

export type StripeCustomerState = AxiosState<Stripe.customers.ICustomer>;
