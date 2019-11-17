import { AxiosActionCreator, AxiosAction } from "utils/axios/types";

export const FORGOT_USER_PASSWORD = "FORGOT_USER_PASSWORD";
export const FORGOT_USER_PASSWORD_SUCCESS = "FORGOT_USER_PASSWORD_SUCCESS";
export const FORGOT_USER_PASSWORD_FAIL = "FORGOT_USER_PASSWORD_FAIL";

export const RESET_USER_PASSWORD = "RESET_USER_PASSWORD";
export const RESET_USER_PASSWORD_SUCCESS = "RESET_USER_PASSWORD_SUCCESS";
export const RESET_USER_PASSWORD_FAIL = "RESET_USER_PASSWORD_FAIL";

export type ForgotUserPasswordType = typeof FORGOT_USER_PASSWORD | typeof FORGOT_USER_PASSWORD_SUCCESS | typeof FORGOT_USER_PASSWORD_FAIL;
export type ForgotUserPasswordActionCreator = AxiosActionCreator<ForgotUserPasswordType>;
export type ForgotUserPasswordAction = AxiosAction<ForgotUserPasswordType>;

export type ResetUserPasswordType = typeof RESET_USER_PASSWORD | typeof RESET_USER_PASSWORD_SUCCESS | typeof RESET_USER_PASSWORD_FAIL;
export type ResetUserPasswordActionCreator = AxiosActionCreator<ResetUserPasswordType>;
export type ResetUserPasswordAction = AxiosAction<ResetUserPasswordType>;

export type UserPasswordActionCreators = ForgotUserPasswordActionCreator | ResetUserPasswordActionCreator;
export type UserPasswordActions = ForgotUserPasswordAction | ResetUserPasswordAction;
