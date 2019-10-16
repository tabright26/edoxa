import { AxiosActionCreator, AxiosAction } from "store/middlewares/axios/types";

export const FORGOT_USER_PASSWORD = "FORGOT_USER_PASSWORD";
export const FORGOT_USER_PASSWORD_SUCCESS = "FORGOT_USER_PASSWORD_SUCCESS";
export const FORGOT_USER_PASSWORD_FAIL = "FORGOT_USER_PASSWORD_FAIL";

export const RESET_USER_PASSWORD = "RESET_USER_PASSWORD";
export const RESET_USER_PASSWORD_SUCCESS = "RESET_USER_PASSWORD_SUCCESS";
export const RESET_USER_PASSWORD_FAIL = "RESET_USER_PASSWORD_FAIL";

type ForgotUserPasswordType = typeof FORGOT_USER_PASSWORD | typeof FORGOT_USER_PASSWORD_SUCCESS | typeof FORGOT_USER_PASSWORD_FAIL;

interface ForgotUserPasswordActionCreator extends AxiosActionCreator<ForgotUserPasswordType> {}

interface ForgotUserPasswordAction extends AxiosAction<ForgotUserPasswordType> {}

type ResetUserPasswordType = typeof RESET_USER_PASSWORD | typeof RESET_USER_PASSWORD_SUCCESS | typeof RESET_USER_PASSWORD_FAIL;

interface ResetUserPasswordActionCreator extends AxiosActionCreator<ResetUserPasswordType> {}

interface ResetUserPasswordAction extends AxiosAction<ResetUserPasswordType> {}

export type UserPasswordActionCreators = ForgotUserPasswordActionCreator | ResetUserPasswordActionCreator;

export type UserPasswordActions = ForgotUserPasswordAction | ResetUserPasswordAction;
