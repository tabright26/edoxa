import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_PAYMENTMETHODS = "LOAD_PAYMENTMETHODS";
export const LOAD_PAYMENTMETHODS_SUCCESS = "LOAD_PAYMENTMETHODS_SUCCESS";
export const LOAD_PAYMENTMETHODS_FAIL = "LOAD_PAYMENTMETHODS_FAIL";

export const UPDATE_PAYMENTMETHOD = "UPDATE_PAYMENTMETHOD";
export const UPDATE_PAYMENTMETHOD_SUCCESS = "UPDATE_PAYMENTMETHOD_SUCCESS";
export const UPDATE_PAYMENTMETHOD_FAIL = "UPDATE_PAYMENTMETHOD_FAIL";

export const ATTACH_PAYMENTMETHOD = "ATTACH_PAYMENTMETHOD";
export const ATTACH_PAYMENTMETHOD_SUCCESS = "ATTACH_PAYMENTMETHOD_SUCCESS";
export const ATTACH_PAYMENTMETHOD_FAIL = "ATTACH_PAYMENTMETHOD_FAIL";

export const DETACH_PAYMENTMETHOD = "DETACH_PAYMENTMETHOD";
export const DETACH_PAYMENTMETHOD_SUCCESS = "DETACH_PAYMENTMETHOD_SUCCESS";
export const DETACH_PAYMENTMETHOD_FAIL = "DETACH_PAYMENTMETHOD_FAIL";

type LoadPaymentMethodsType = typeof LOAD_PAYMENTMETHODS | typeof LOAD_PAYMENTMETHODS_SUCCESS | typeof LOAD_PAYMENTMETHODS_FAIL;

interface LoadPaymentMethodsActionCreator extends AxiosActionCreator<LoadPaymentMethodsType> {}

interface LoadPaymentMethodsAction extends AxiosAction<LoadPaymentMethodsType> {}

type UpdatePaymentMethodType = typeof UPDATE_PAYMENTMETHOD | typeof UPDATE_PAYMENTMETHOD_SUCCESS | typeof UPDATE_PAYMENTMETHOD_FAIL;

interface UpdatePaymentMethodActionCreator extends AxiosActionCreator<UpdatePaymentMethodType> {}

interface UpdatePaymentMethodAction extends AxiosAction<UpdatePaymentMethodType> {}

type AttachPaymentMethodType = typeof ATTACH_PAYMENTMETHOD | typeof ATTACH_PAYMENTMETHOD_SUCCESS | typeof ATTACH_PAYMENTMETHOD_FAIL;

interface AttachPaymentMethodActionCreator extends AxiosActionCreator<AttachPaymentMethodType> {}

interface AttachPaymentMethodAction extends AxiosAction<AttachPaymentMethodType> {}

type DetachPaymentMethodType = typeof DETACH_PAYMENTMETHOD | typeof DETACH_PAYMENTMETHOD_SUCCESS | typeof DETACH_PAYMENTMETHOD_FAIL;

interface DetachPaymentMethodActionCreator extends AxiosActionCreator<DetachPaymentMethodType> {}

interface DetachPaymentMethodAction extends AxiosAction<DetachPaymentMethodType> {}

export type PaymentMethodsActionCreators = LoadPaymentMethodsActionCreator | AttachPaymentMethodActionCreator | DetachPaymentMethodActionCreator | UpdatePaymentMethodActionCreator;

export type PaymentMethodsActionTypes = LoadPaymentMethodsAction | AttachPaymentMethodAction | DetachPaymentMethodAction | UpdatePaymentMethodAction;

export type PaymentMethodsState = AxiosState;

export const CARD_PAYMENTMETHOD_TYPE = "card";

export type PaymentMethodType = typeof CARD_PAYMENTMETHOD_TYPE;
