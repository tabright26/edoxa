import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { StripePaymentMethod } from "types";

export const LOAD_STRIPE_PAYMENTMETHODS = "LOAD_STRIPE_PAYMENTMETHODS";
export const LOAD_STRIPE_PAYMENTMETHODS_SUCCESS = "LOAD_STRIPE_PAYMENTMETHODS_SUCCESS";
export const LOAD_STRIPE_PAYMENTMETHODS_FAIL = "LOAD_STRIPE_PAYMENTMETHODS_FAIL";

export const UPDATE_STRIPE_PAYMENTMETHOD = "UPDATE_STRIPE_PAYMENTMETHOD";
export const UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS = "UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS";
export const UPDATE_STRIPE_PAYMENTMETHOD_FAIL = "UPDATE_STRIPE_PAYMENTMETHOD_FAIL";

export const ATTACH_STRIPE_PAYMENTMETHOD = "ATTACH_STRIPE_PAYMENTMETHOD";
export const ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS = "ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS";
export const ATTACH_STRIPE_PAYMENTMETHOD_FAIL = "ATTACH_STRIPE_PAYMENTMETHOD_FAIL";

export const DETACH_STRIPE_PAYMENTMETHOD = "DETACH_STRIPE_PAYMENTMETHOD";
export const DETACH_STRIPE_PAYMENTMETHOD_SUCCESS = "DETACH_STRIPE_PAYMENTMETHOD_SUCCESS";
export const DETACH_STRIPE_PAYMENTMETHOD_FAIL = "DETACH_STRIPE_PAYMENTMETHOD_FAIL";

export type LoadStripePaymentMethodsType = typeof LOAD_STRIPE_PAYMENTMETHODS | typeof LOAD_STRIPE_PAYMENTMETHODS_SUCCESS | typeof LOAD_STRIPE_PAYMENTMETHODS_FAIL;

export type LoadStripePaymentMethodsActionCreator = AxiosActionCreator<LoadStripePaymentMethodsType>;

export type LoadStripePaymentMethodsAction = AxiosAction<LoadStripePaymentMethodsType, StripePaymentMethod[]>;

export type UpdateStripePaymentMethodType = typeof UPDATE_STRIPE_PAYMENTMETHOD | typeof UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS | typeof UPDATE_STRIPE_PAYMENTMETHOD_FAIL;

export type UpdateStripePaymentMethodActionCreator = AxiosActionCreator<UpdateStripePaymentMethodType>;

export type UpdateStripePaymentMethodAction = AxiosAction<UpdateStripePaymentMethodType, StripePaymentMethod>;

export type AttachStripePaymentMethodType = typeof ATTACH_STRIPE_PAYMENTMETHOD | typeof ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS | typeof ATTACH_STRIPE_PAYMENTMETHOD_FAIL;

export type AttachStripePaymentMethodActionCreator = AxiosActionCreator<AttachStripePaymentMethodType>;

export type AttachStripePaymentMethodAction = AxiosAction<AttachStripePaymentMethodType, StripePaymentMethod>;

export type DetachStripePaymentMethodType = typeof DETACH_STRIPE_PAYMENTMETHOD | typeof DETACH_STRIPE_PAYMENTMETHOD_SUCCESS | typeof DETACH_STRIPE_PAYMENTMETHOD_FAIL;

export type DetachStripePaymentMethodActionCreator = AxiosActionCreator<DetachStripePaymentMethodType>;

export type DetachStripePaymentMethodAction = AxiosAction<DetachStripePaymentMethodType, StripePaymentMethod>;

export type StripePaymentMethodsActionCreators =
  | LoadStripePaymentMethodsActionCreator
  | AttachStripePaymentMethodActionCreator
  | DetachStripePaymentMethodActionCreator
  | UpdateStripePaymentMethodActionCreator;

export type StripePaymentMethodsActions = LoadStripePaymentMethodsAction | AttachStripePaymentMethodAction | DetachStripePaymentMethodAction | UpdateStripePaymentMethodAction;

export type StripePaymentMethodsState = AxiosState<StripePaymentMethod[]>;
