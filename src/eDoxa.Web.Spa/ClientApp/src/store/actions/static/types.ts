import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import {
  IdentityStaticOptions,
  PaymentStaticOptions,
  CashierStaticOptions
} from "types";

export const LOAD_IDENTITY_STATIC_OPTIONS = "LOAD_IDENTITY_STATIC_OPTIONS";
export const LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS =
  "LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS";
export const LOAD_IDENTITY_STATIC_OPTIONS_FAIL =
  "LOAD_IDENTITY_STATIC_OPTIONS_FAIL";

export type LoadIdentityStaticOptionsType =
  | typeof LOAD_IDENTITY_STATIC_OPTIONS
  | typeof LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS
  | typeof LOAD_IDENTITY_STATIC_OPTIONS_FAIL;

export type LoadIdentityStaticOptionsActionCreator = AxiosActionCreator<
  LoadIdentityStaticOptionsType
>;

export type LoadIdentityStaticOptionsAction = AxiosAction<
  LoadIdentityStaticOptionsType,
  IdentityStaticOptions
>;

export const LOAD_PAYMENT_STATIC_OPTIONS = "LOAD_PAYMENT_STATIC_OPTIONS";
export const LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS =
  "LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS";
export const LOAD_PAYMENT_STATIC_OPTIONS_FAIL =
  "LOAD_PAYMENT_STATIC_OPTIONS_FAIL";

export type LoadPaymentStaticOptionsType =
  | typeof LOAD_PAYMENT_STATIC_OPTIONS
  | typeof LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS
  | typeof LOAD_PAYMENT_STATIC_OPTIONS_FAIL;

export type LoadPaymentStaticOptionsActionCreator = AxiosActionCreator<
  LoadPaymentStaticOptionsType
>;

export type LoadPaymentStaticOptionsAction = AxiosAction<
  LoadPaymentStaticOptionsType,
  PaymentStaticOptions
>;

export const LOAD_CASHIER_STATIC_OPTIONS = "LOAD_CASHIER_STATIC_OPTIONS";
export const LOAD_CASHIER_STATIC_OPTIONS_SUCCESS =
  "LOAD_CASHIER_STATIC_OPTIONS_SUCCESS";
export const LOAD_CASHIER_STATIC_OPTIONS_FAIL =
  "LOAD_CASHIER_STATIC_OPTIONS_FAIL";

export type LoadCashierStaticOptionsType =
  | typeof LOAD_CASHIER_STATIC_OPTIONS
  | typeof LOAD_CASHIER_STATIC_OPTIONS_SUCCESS
  | typeof LOAD_CASHIER_STATIC_OPTIONS_FAIL;
export type LoadCashierStaticOptionsActionCreator = AxiosActionCreator<
  LoadCashierStaticOptionsType
>;
export type LoadCashierStaticOptionsAction = AxiosAction<
  LoadCashierStaticOptionsType,
  CashierStaticOptions
>;

export type StaticOptionsActionCreators =
  | LoadIdentityStaticOptionsActionCreator
  | LoadPaymentStaticOptionsActionCreator
  | LoadCashierStaticOptionsActionCreator;

export type StaticOptionsActions =
  | LoadIdentityStaticOptionsAction
  | LoadPaymentStaticOptionsAction
  | LoadCashierStaticOptionsAction;
