import { AxiosActionCreator, AxiosAction } from "interfaces/axios";
import { Currency } from "../types";

export const LOAD_WITHDRAWAL_MONEY_AMOUNTS = "LOAD_WITHDRAWAL_MONEY_AMOUNTS";
export const LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS = "LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS";
export const LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL = "LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL";

type LoadWithdrawalMoneyAmountsType = typeof LOAD_WITHDRAWAL_MONEY_AMOUNTS | typeof LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS | typeof LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL;

interface LoadWithdrawalMoneyAmountsActionCreator extends AxiosActionCreator<LoadWithdrawalMoneyAmountsType> {}

interface LoadWithdrawalMoneyAmountsAction extends AxiosAction<LoadWithdrawalMoneyAmountsType> {}

export const WITHDRAWAL_TOKEN = "WITHDRAWAL_TOKEN";
export const WITHDRAWAL_TOKEN_SUCCESS = "WITHDRAWAL_TOKEN_SUCCESS";
export const WITHDRAWAL_TOKEN_FAIL = "WITHDRAWAL_TOKEN_FAIL";

type WithdrawalTokenType = typeof WITHDRAWAL_TOKEN | typeof WITHDRAWAL_TOKEN_SUCCESS | typeof WITHDRAWAL_TOKEN_FAIL;

interface WithdrawalMoneyActionCreator extends AxiosActionCreator<WithdrawalTokenType> {}

interface WithdrawalMoneyAction extends AxiosAction<WithdrawalTokenType> {}

export type WithdrawalActionCreators = WithdrawalMoneyActionCreator | LoadWithdrawalMoneyAmountsActionCreator;

export type WithdrawalActionTypes = WithdrawalMoneyAction | LoadWithdrawalMoneyAmountsAction;

export interface WithdrawalState {
  amounts: Map<Currency, number[]>;
}
