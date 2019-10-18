import { AxiosActionCreator, AxiosAction } from "store/middlewares/axios/types";

export const USER_ACCOUNT_DEPOSIT_MONEY = "USER_ACCOUNT_DEPOSIT_MONEY";
export const USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS = "USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS";
export const USER_ACCOUNT_DEPOSIT_MONEY_FAIL = "USER_ACCOUNT_DEPOSIT_MONEY_FAIL";

export type UserAccountDepositMoneyType = typeof USER_ACCOUNT_DEPOSIT_MONEY | typeof USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS | typeof USER_ACCOUNT_DEPOSIT_MONEY_FAIL;

interface UserAccountDepositMoneyActionCreator extends AxiosActionCreator<UserAccountDepositMoneyType> {}

interface UserAccountDepositMoneyAction extends AxiosAction<UserAccountDepositMoneyType> {}

export const USER_ACCOUNT_DEPOSIT_TOKEN = "USER_ACCOUNT_DEPOSIT_TOKEN";
export const USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS = "USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS";
export const USER_ACCOUNT_DEPOSIT_TOKEN_FAIL = "USER_ACCOUNT_DEPOSIT_TOKEN_FAIL";

export type UserAccountDepositTokenType = typeof USER_ACCOUNT_DEPOSIT_TOKEN | typeof USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS | typeof USER_ACCOUNT_DEPOSIT_TOKEN_FAIL;

interface UserAccountDepositTokenActionCreator extends AxiosActionCreator<UserAccountDepositTokenType> {}

interface UserAccountDepositTokenAction extends AxiosAction<UserAccountDepositTokenType> {}

export type UserAccountDepositActionCreators = UserAccountDepositMoneyActionCreator | UserAccountDepositTokenActionCreator;

export type UserAccountDepositActions = UserAccountDepositMoneyAction | UserAccountDepositTokenAction;
