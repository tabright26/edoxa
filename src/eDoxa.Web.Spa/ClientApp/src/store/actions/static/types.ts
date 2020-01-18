import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import {
  IdentityStaticOptions,
  PaymentStaticOptions,
  TransactionBundle
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

export const LOAD_TRANSACTION_BUNDLES = "LOAD_TRANSACTION_BUNDLES";
export const LOAD_TRANSACTION_BUNDLES_SUCCESS =
  "LOAD_TRANSACTION_BUNDLES_SUCCESS";
export const LOAD_TRANSACTION_BUNDLES_FAIL = "LOAD_TRANSACTION_BUNDLES_FAIL";

export type LoadTransactionBundlesType =
  | typeof LOAD_TRANSACTION_BUNDLES
  | typeof LOAD_TRANSACTION_BUNDLES_SUCCESS
  | typeof LOAD_TRANSACTION_BUNDLES_FAIL;
export type LoadTransactionBundlesActionCreator = AxiosActionCreator<
  LoadTransactionBundlesType
>;
export type LoadTransactionBundlesAction = AxiosAction<
  LoadTransactionBundlesType,
  TransactionBundle[]
>;

export type TransactionBundlesActionCreators = LoadTransactionBundlesActionCreator;
export type TransactionBundlesActions = LoadTransactionBundlesAction;

export type StaticOptionsActionCreators =
  | LoadIdentityStaticOptionsActionCreator
  | LoadPaymentStaticOptionsActionCreator
  | LoadTransactionBundlesActionCreator;

export type StaticOptionsActions =
  | LoadIdentityStaticOptionsAction
  | LoadPaymentStaticOptionsAction
  | LoadTransactionBundlesAction;
