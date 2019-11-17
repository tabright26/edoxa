import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import { Transaction } from "types";

export const USER_ACCOUNT_DEPOSIT_MONEY = "USER_ACCOUNT_DEPOSIT_MONEY";
export const USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS = "USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS";
export const USER_ACCOUNT_DEPOSIT_MONEY_FAIL = "USER_ACCOUNT_DEPOSIT_MONEY_FAIL";

export const USER_ACCOUNT_DEPOSIT_TOKEN = "USER_ACCOUNT_DEPOSIT_TOKEN";
export const USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS = "USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS";
export const USER_ACCOUNT_DEPOSIT_TOKEN_FAIL = "USER_ACCOUNT_DEPOSIT_TOKEN_FAIL";

export type UserAccountDepositMoneyType = typeof USER_ACCOUNT_DEPOSIT_MONEY | typeof USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS | typeof USER_ACCOUNT_DEPOSIT_MONEY_FAIL;
export type UserAccountDepositMoneyActionCreator = AxiosActionCreator<UserAccountDepositMoneyType>;
export type UserAccountDepositMoneyAction = AxiosAction<UserAccountDepositMoneyType, Transaction>;

export type UserAccountDepositTokenType = typeof USER_ACCOUNT_DEPOSIT_TOKEN | typeof USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS | typeof USER_ACCOUNT_DEPOSIT_TOKEN_FAIL;
export type UserAccountDepositTokenActionCreator = AxiosActionCreator<UserAccountDepositTokenType>;
export type UserAccountDepositTokenAction = AxiosAction<UserAccountDepositTokenType, Transaction>;

export type UserAccountDepositActionCreators = UserAccountDepositMoneyActionCreator | UserAccountDepositTokenActionCreator;
export type UserAccountDepositActions = UserAccountDepositMoneyAction | UserAccountDepositTokenAction;
