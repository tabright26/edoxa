import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";
import { Currency } from "types";

export const LOAD_WITHDRAWAL_MONEY_AMOUNTS = "LOAD_WITHDRAWAL_MONEY_AMOUNTS";
export const LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS = "LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS";
export const LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL = "LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL";

type LoadWithdrawalMoneyAmountsType = typeof LOAD_WITHDRAWAL_MONEY_AMOUNTS | typeof LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS | typeof LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL;

interface LoadWithdrawalMoneyAmountsActionCreator extends AxiosActionCreator<LoadWithdrawalMoneyAmountsType> {}

interface LoadWithdrawalMoneyAmountsAction extends AxiosAction<LoadWithdrawalMoneyAmountsType> {}

export const WITHDRAWAL_MONEY = "WITHDRAWAL_MONEY";
export const WITHDRAWAL_MONEY_SUCCESS = "WITHDRAWAL_MONEY_SUCCESS";
export const WITHDRAWAL_MONEY_FAIL = "WITHDRAWAL_MONEY_FAIL";

type WithdrawalMoneyType = typeof WITHDRAWAL_MONEY | typeof WITHDRAWAL_MONEY_SUCCESS | typeof WITHDRAWAL_MONEY_FAIL;

interface WithdrawalMoneyActionCreator extends AxiosActionCreator<WithdrawalMoneyType> {}

interface WithdrawalMoneyAction extends AxiosAction<WithdrawalMoneyType> {}

export type WithdrawalActionCreators = WithdrawalMoneyActionCreator | LoadWithdrawalMoneyAmountsActionCreator;

export type WithdrawalActions = WithdrawalMoneyAction | LoadWithdrawalMoneyAmountsAction;

export type WithdrawalState = AxiosState<{ amounts: Map<Currency, number[]> }>;
