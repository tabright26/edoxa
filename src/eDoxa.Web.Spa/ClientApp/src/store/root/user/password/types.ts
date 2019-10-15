import { AxiosActionCreator, AxiosAction } from "store/middlewares/axios/types";

export const FORGOT_PASSWORD = "FORGOT_PASSWORD";
export const FORGOT_PASSWORD_SUCCESS = "FORGOT_PASSWORD_SUCCESS";
export const FORGOT_PASSWORD_FAIL = "FORGOT_PASSWORD_FAIL";

export const RESET_PASSWORD = "RESET_PASSWORD";
export const RESET_PASSWORD_SUCCESS = "RESET_PASSWORD_SUCCESS";
export const RESET_PASSWORD_FAIL = "RESET_PASSWORD_FAIL";

type ForgotPasswordType = typeof FORGOT_PASSWORD | typeof FORGOT_PASSWORD_SUCCESS | typeof FORGOT_PASSWORD_FAIL;

interface ForgotPasswordActionCreator extends AxiosActionCreator<ForgotPasswordType> {}

interface ForgotPasswordAction extends AxiosAction<ForgotPasswordType> {}

type ResetPasswordType = typeof RESET_PASSWORD | typeof RESET_PASSWORD_SUCCESS | typeof RESET_PASSWORD_FAIL;

interface ResetPasswordActionCreator extends AxiosActionCreator<ResetPasswordType> {}

interface ResetPasswordAction extends AxiosAction<ResetPasswordType> {}

export type PasswordActionCreators = ForgotPasswordActionCreator | ResetPasswordActionCreator;

export type PasswordActionTypes = ForgotPasswordAction | ResetPasswordAction;

export interface PasswordState {}
