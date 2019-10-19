import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";

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

export const STRIPE_PAYMENTMETHOD_CARD_TYPE = "card";

type LoadStripePaymentMethodsType = typeof LOAD_STRIPE_PAYMENTMETHODS | typeof LOAD_STRIPE_PAYMENTMETHODS_SUCCESS | typeof LOAD_STRIPE_PAYMENTMETHODS_FAIL;

interface LoadStripePaymentMethodsActionCreator extends AxiosActionCreator<LoadStripePaymentMethodsType> {}

interface LoadStripePaymentMethodsAction extends AxiosAction<LoadStripePaymentMethodsType> {}

type UpdateStripePaymentMethodType = typeof UPDATE_STRIPE_PAYMENTMETHOD | typeof UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS | typeof UPDATE_STRIPE_PAYMENTMETHOD_FAIL;

interface UpdateStripePaymentMethodActionCreator extends AxiosActionCreator<UpdateStripePaymentMethodType> {}

interface UpdateStripePaymentMethodAction extends AxiosAction<UpdateStripePaymentMethodType> {}

type AttachStripePaymentMethodType = typeof ATTACH_STRIPE_PAYMENTMETHOD | typeof ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS | typeof ATTACH_STRIPE_PAYMENTMETHOD_FAIL;

interface AttachStripePaymentMethodActionCreator extends AxiosActionCreator<AttachStripePaymentMethodType> {}

interface AttachStripePaymentMethodAction extends AxiosAction<AttachStripePaymentMethodType> {}

type DetachStripePaymentMethodType = typeof DETACH_STRIPE_PAYMENTMETHOD | typeof DETACH_STRIPE_PAYMENTMETHOD_SUCCESS | typeof DETACH_STRIPE_PAYMENTMETHOD_FAIL;

interface DetachStripePaymentMethodActionCreator extends AxiosActionCreator<DetachStripePaymentMethodType> {}

interface DetachStripePaymentMethodAction extends AxiosAction<DetachStripePaymentMethodType> {}

export type StripePaymentMethodsActionCreators =
  | LoadStripePaymentMethodsActionCreator
  | AttachStripePaymentMethodActionCreator
  | DetachStripePaymentMethodActionCreator
  | UpdateStripePaymentMethodActionCreator;

export type StripePaymentMethodsActions = LoadStripePaymentMethodsAction | AttachStripePaymentMethodAction | DetachStripePaymentMethodAction | UpdateStripePaymentMethodAction;

export type StripePaymentMethodsState = AxiosState;

export type StripePaymentMethodType = typeof STRIPE_PAYMENTMETHOD_CARD_TYPE;
