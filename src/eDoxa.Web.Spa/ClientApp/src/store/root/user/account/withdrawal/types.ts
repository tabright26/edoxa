import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import { Transaction } from "types";

export const USER_ACCOUNT_WITHDRAWAL_MONEY = "USER_ACCOUNT_WITHDRAWAL_MONEY";
export const USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS = "USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS";
export const USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL = "USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL";

export type UserAccountWithdrawalMoneyType = typeof USER_ACCOUNT_WITHDRAWAL_MONEY | typeof USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS | typeof USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL;
export type UserAccountWithdrawalMoneyActionCreator = AxiosActionCreator<UserAccountWithdrawalMoneyType>;
export type UserAccountWithdrawalMoneyAction = AxiosAction<UserAccountWithdrawalMoneyType, Transaction>;

export type UserAccountWithdrawalActionCreators = UserAccountWithdrawalMoneyActionCreator;
export type UserAccountWithdrawalActions = UserAccountWithdrawalMoneyAction;
