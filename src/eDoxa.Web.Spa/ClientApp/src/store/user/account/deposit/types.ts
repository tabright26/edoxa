import { AxiosActionCreator, AxiosAction } from "interfaces/axios";
import { Currency } from "../types";

export const LOAD_DEPOSIT_MONEY_AMOUNTS = "LOAD_DEPOSIT_MONEY_AMOUNTS";
export const LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS = "LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS";
export const LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL = "LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL";

type LoadDepositMoneyAmountsType = typeof LOAD_DEPOSIT_MONEY_AMOUNTS | typeof LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS | typeof LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL;

interface LoadDepositMoneyAmountsActionCreator extends AxiosActionCreator<LoadDepositMoneyAmountsType> {}

interface LoadDepositMoneyAmountsAction extends AxiosAction<LoadDepositMoneyAmountsType> {}

export const LOAD_DEPOSIT_TOKEN_AMOUNTS = "LOAD_DEPOSIT_TOKEN_AMOUNTS";
export const LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS = "LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS";
export const LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL = "LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL";

type LoadDepositTokenAmountsType = typeof LOAD_DEPOSIT_TOKEN_AMOUNTS | typeof LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS | typeof LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL;

interface LoadDepositTokenAmountsActionCreator extends AxiosActionCreator<LoadDepositTokenAmountsType> {}

interface LoadDepositTokenAmountsAction extends AxiosAction<LoadDepositTokenAmountsType> {}

export const DEPOSIT_MONEY = "DEPOSIT_MONEY";
export const DEPOSIT_MONEY_SUCCESS = "DEPOSIT_MONEY_SUCCESS";
export const DEPOSIT_MONEY_FAIL = "DEPOSIT_MONEY_FAIL";

export type DepositMoneyType = typeof DEPOSIT_MONEY | typeof DEPOSIT_MONEY_SUCCESS | typeof DEPOSIT_MONEY_FAIL;

interface DepositMoneyActionCreator extends AxiosActionCreator<DepositMoneyType> {}

interface DepositMoneyAction extends AxiosAction<DepositMoneyType> {}

export const DEPOSIT_TOKEN = "DEPOSIT_TOKEN";
export const DEPOSIT_TOKEN_SUCCESS = "DEPOSIT_TOKEN_SUCCESS";
export const DEPOSIT_TOKEN_FAIL = "DEPOSIT_TOKEN_FAIL";

export type DepositTokenType = typeof DEPOSIT_TOKEN | typeof DEPOSIT_TOKEN_SUCCESS | typeof DEPOSIT_TOKEN_FAIL;

interface DepositTokenActionCreator extends AxiosActionCreator<DepositTokenType> {}

interface DepositTokenAction extends AxiosAction<DepositTokenType> {}

export type DepositActionCreators = LoadDepositMoneyAmountsActionCreator | LoadDepositTokenAmountsActionCreator | DepositMoneyActionCreator | DepositTokenActionCreator;

export type DepositActionTypes = LoadDepositMoneyAmountsAction | LoadDepositTokenAmountsAction | DepositMoneyAction | DepositTokenAction;

export interface DepositState {
  amounts: Map<Currency, number[]>;
}
