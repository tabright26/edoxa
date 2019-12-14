import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import {
  StripeAccount,
  StripeCustomer,
  StripePaymentMethod,
  StripeBankAccount
} from "types";

export const LOAD_STRIPE_ACCOUNT = "LOAD_STRIPE_ACCOUNT";
export const LOAD_STRIPE_ACCOUNT_SUCCESS = "LOAD_STRIPE_ACCOUNT_SUCCESS";
export const LOAD_STRIPE_ACCOUNT_FAIL = "LOAD_STRIPE_ACCOUNT_FAIL";

export type LoadStripeAccountType =
  | typeof LOAD_STRIPE_ACCOUNT
  | typeof LOAD_STRIPE_ACCOUNT_SUCCESS
  | typeof LOAD_STRIPE_ACCOUNT_FAIL;

export type LoadStripeAccountActionCreator = AxiosActionCreator<
  LoadStripeAccountType
>;

export type LoadStripeAccountAction = AxiosAction<
  LoadStripeAccountType,
  StripeAccount
>;

export type StripeAccountActionCreators = LoadStripeAccountActionCreator;

export type StripeAccountActions = LoadStripeAccountAction;

export type StripeAccountState = AxiosState<StripeAccount>;

export const LOAD_STRIPE_BANKACCOUNT = "LOAD_STRIPE_BANKACCOUNT";
export const LOAD_STRIPE_BANKACCOUNT_SUCCESS =
  "LOAD_STRIPE_BANKACCOUNT_SUCCESS";
export const LOAD_STRIPE_BANKACCOUNT_FAIL = "LOAD_STRIPE_BANKACCOUNT_FAIL";

export const UPDATE_STRIPE_BANKACCOUNT = "UPDATE_STRIPE_BANKACCOUNT";
export const UPDATE_STRIPE_BANKACCOUNT_SUCCESS =
  "UPDATE_STRIPE_BANKACCOUNT_SUCCESS";
export const UPDATE_STRIPE_BANKACCOUNT_FAIL = "UPDATE_STRIPE_BANKACCOUNT_FAIL";

export type LoadStripeBankAccountType =
  | typeof LOAD_STRIPE_BANKACCOUNT
  | typeof LOAD_STRIPE_BANKACCOUNT_SUCCESS
  | typeof LOAD_STRIPE_BANKACCOUNT_FAIL;

export type LoadStripeBankAccountActionCreator = AxiosActionCreator<
  LoadStripeBankAccountType
>;

export type LoadStripeBankAccountAction = AxiosAction<
  LoadStripeBankAccountType,
  StripeBankAccount
>;

export type UpdateStripeBankAccountType =
  | typeof UPDATE_STRIPE_BANKACCOUNT
  | typeof UPDATE_STRIPE_BANKACCOUNT_SUCCESS
  | typeof UPDATE_STRIPE_BANKACCOUNT_FAIL;

export type UpdateStripeBankAccountActionCreator = AxiosActionCreator<
  UpdateStripeBankAccountType
>;

export type UpdateStripeBankAccountAction = AxiosAction<
  UpdateStripeBankAccountType,
  StripeBankAccount
>;

export type StripeBankAccountActionCreators =
  | LoadStripeBankAccountActionCreator
  | UpdateStripeBankAccountActionCreator;

export type StripeBankAccountActions =
  | LoadStripeBankAccountAction
  | UpdateStripeBankAccountAction;

export type StripeBankAccountState = AxiosState<StripeBankAccount>;

export const LOAD_STRIPE_CUSTOMER = "LOAD_STRIPE_CUSTOMER";
export const LOAD_STRIPE_CUSTOMER_SUCCESS = "LOAD_STRIPE_CUSTOMER_SUCCESS";
export const LOAD_STRIPE_CUSTOMER_FAIL = "LOAD_STRIPE_CUSTOMER_FAIL";

export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD =
  "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD";
export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS =
  "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS";
export const UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL =
  "UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL";

export type LoadStripeCustomerType =
  | typeof LOAD_STRIPE_CUSTOMER
  | typeof LOAD_STRIPE_CUSTOMER_SUCCESS
  | typeof LOAD_STRIPE_CUSTOMER_FAIL;

export type LoadStripeCustomerActionCreator = AxiosActionCreator<
  LoadStripeCustomerType
>;

export type LoadStripeCustomerAction = AxiosAction<
  LoadStripeCustomerType,
  StripeCustomer
>;

export type UpdateStripeCustomerDefaultPaymentMethodType =
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS
  | typeof UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL;

export type UpdateStripeCustomerDefaultPaymentMethodActionCreator = AxiosActionCreator<
  UpdateStripeCustomerDefaultPaymentMethodType
>;

export type UpdateStripeCustomerDefaultPaymentMethodAction = AxiosAction<
  UpdateStripeCustomerDefaultPaymentMethodType,
  StripeCustomer
>;

export type StripeCustomerActionCreators =
  | LoadStripeCustomerActionCreator
  | UpdateStripeCustomerDefaultPaymentMethodActionCreator;

export type StripeCustomerActions =
  | LoadStripeCustomerAction
  | UpdateStripeCustomerDefaultPaymentMethodAction;

export type StripeCustomerState = AxiosState<StripeCustomer>;

export const LOAD_STRIPE_PAYMENTMETHODS = "LOAD_STRIPE_PAYMENTMETHODS";
export const LOAD_STRIPE_PAYMENTMETHODS_SUCCESS =
  "LOAD_STRIPE_PAYMENTMETHODS_SUCCESS";
export const LOAD_STRIPE_PAYMENTMETHODS_FAIL =
  "LOAD_STRIPE_PAYMENTMETHODS_FAIL";

export const UPDATE_STRIPE_PAYMENTMETHOD = "UPDATE_STRIPE_PAYMENTMETHOD";
export const UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS =
  "UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS";
export const UPDATE_STRIPE_PAYMENTMETHOD_FAIL =
  "UPDATE_STRIPE_PAYMENTMETHOD_FAIL";

export const ATTACH_STRIPE_PAYMENTMETHOD = "ATTACH_STRIPE_PAYMENTMETHOD";
export const ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS =
  "ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS";
export const ATTACH_STRIPE_PAYMENTMETHOD_FAIL =
  "ATTACH_STRIPE_PAYMENTMETHOD_FAIL";

export const DETACH_STRIPE_PAYMENTMETHOD = "DETACH_STRIPE_PAYMENTMETHOD";
export const DETACH_STRIPE_PAYMENTMETHOD_SUCCESS =
  "DETACH_STRIPE_PAYMENTMETHOD_SUCCESS";
export const DETACH_STRIPE_PAYMENTMETHOD_FAIL =
  "DETACH_STRIPE_PAYMENTMETHOD_FAIL";

export type LoadStripePaymentMethodsType =
  | typeof LOAD_STRIPE_PAYMENTMETHODS
  | typeof LOAD_STRIPE_PAYMENTMETHODS_SUCCESS
  | typeof LOAD_STRIPE_PAYMENTMETHODS_FAIL;

export type LoadStripePaymentMethodsActionCreator = AxiosActionCreator<
  LoadStripePaymentMethodsType
>;

export type LoadStripePaymentMethodsAction = AxiosAction<
  LoadStripePaymentMethodsType,
  StripePaymentMethod[]
>;

export type UpdateStripePaymentMethodType =
  | typeof UPDATE_STRIPE_PAYMENTMETHOD
  | typeof UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS
  | typeof UPDATE_STRIPE_PAYMENTMETHOD_FAIL;

export type UpdateStripePaymentMethodActionCreator = AxiosActionCreator<
  UpdateStripePaymentMethodType
>;

export type UpdateStripePaymentMethodAction = AxiosAction<
  UpdateStripePaymentMethodType,
  StripePaymentMethod
>;

export type AttachStripePaymentMethodType =
  | typeof ATTACH_STRIPE_PAYMENTMETHOD
  | typeof ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS
  | typeof ATTACH_STRIPE_PAYMENTMETHOD_FAIL;

export type AttachStripePaymentMethodActionCreator = AxiosActionCreator<
  AttachStripePaymentMethodType
>;

export type AttachStripePaymentMethodAction = AxiosAction<
  AttachStripePaymentMethodType,
  StripePaymentMethod
>;

export type DetachStripePaymentMethodType =
  | typeof DETACH_STRIPE_PAYMENTMETHOD
  | typeof DETACH_STRIPE_PAYMENTMETHOD_SUCCESS
  | typeof DETACH_STRIPE_PAYMENTMETHOD_FAIL;

export type DetachStripePaymentMethodActionCreator = AxiosActionCreator<
  DetachStripePaymentMethodType
>;

export type DetachStripePaymentMethodAction = AxiosAction<
  DetachStripePaymentMethodType,
  StripePaymentMethod
>;

export type StripePaymentMethodsActionCreators =
  | LoadStripePaymentMethodsActionCreator
  | AttachStripePaymentMethodActionCreator
  | DetachStripePaymentMethodActionCreator
  | UpdateStripePaymentMethodActionCreator;

export type StripePaymentMethodsActions =
  | LoadStripePaymentMethodsAction
  | AttachStripePaymentMethodAction
  | DetachStripePaymentMethodAction
  | UpdateStripePaymentMethodAction;

export type StripePaymentMethodsState = AxiosState<StripePaymentMethod[]>;
