import { AxiosActionCreator, AxiosAction } from "interfaces/axios";
import { Currency } from "../types";

export const LOAD_WITHDRAWAL_TOKEN_AMOUNTS = "LOAD_WITHDRAWAL_TOKEN_AMOUNTS";
export const LOAD_WITHDRAWAL_TOKEN_AMOUNTS_SUCCESS = "LOAD_WITHDRAWAL_TOKEN_AMOUNTS_SUCCESS";
export const LOAD_WITHDRAWAL_TOKEN_AMOUNTS_FAIL = "LOAD_WITHDRAWAL_TOKEN_AMOUNTS_FAIL";

type LoadWithdrawalTokenAmountsType = typeof LOAD_WITHDRAWAL_TOKEN_AMOUNTS | typeof LOAD_WITHDRAWAL_TOKEN_AMOUNTS_SUCCESS | typeof LOAD_WITHDRAWAL_TOKEN_AMOUNTS_FAIL;

interface LoadWithdrawalTokenAmountsActionCreator extends AxiosActionCreator<LoadWithdrawalTokenAmountsType> {}

interface LoadWithdrawalTokenAmountsAction extends AxiosAction<LoadWithdrawalTokenAmountsType> {}

export const WITHDRAWAL_TOKEN = "WITHDRAWAL_TOKEN";
export const WITHDRAWAL_TOKEN_SUCCESS = "WITHDRAWAL_TOKEN_SUCCESS";
export const WITHDRAWAL_TOKEN_FAIL = "WITHDRAWAL_TOKEN_FAIL";

type WithdrawalTokenType = typeof WITHDRAWAL_TOKEN | typeof WITHDRAWAL_TOKEN_SUCCESS | typeof WITHDRAWAL_TOKEN_FAIL;

interface WithdrawalMoneyActionCreator extends AxiosActionCreator<WithdrawalTokenType> {}

interface WithdrawalMoneyAction extends AxiosAction<WithdrawalTokenType> {}

export type WithdrawalActionCreators = WithdrawalMoneyActionCreator | LoadWithdrawalTokenAmountsActionCreator;

export type WithdrawalActionTypes = WithdrawalMoneyAction | LoadWithdrawalTokenAmountsAction;

export interface WithdrawalState {
  amounts: Map<Currency, number[]>;
}
