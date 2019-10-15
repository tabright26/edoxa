import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_EMAIL = "LOAD_EMAIL";
export const LOAD_EMAIL_SUCCESS = "LOAD_EMAIL_SUCCESS";
export const LOAD_EMAIL_FAIL = "LOAD_EMAIL_FAIL";

export const CONFIRM_EMAIL = "CONFIRM_EMAIL";
export const CONFIRM_EMAIL_SUCCESS = "CONFIRM_EMAIL_SUCCESS";
export const CONFIRM_EMAIL_FAIL = "CONFIRM_EMAIL_FAIL";

type LoadEmailType = typeof LOAD_EMAIL | typeof LOAD_EMAIL_SUCCESS | typeof LOAD_EMAIL_FAIL;

interface LoadEmailActionCreator extends AxiosActionCreator<LoadEmailType> {}

interface LoadEmailAction extends AxiosAction<LoadEmailType> {}

type ConfirmEmailType = typeof CONFIRM_EMAIL | typeof CONFIRM_EMAIL_SUCCESS | typeof CONFIRM_EMAIL_FAIL;

interface ConfirmEmailActionCreator extends AxiosActionCreator<ConfirmEmailType> {}

interface ConfirmEmailAction extends AxiosAction<ConfirmEmailType> {}

export type EmailActionCreators = LoadEmailActionCreator | ConfirmEmailActionCreator;

export type EmailActionTypes = LoadEmailAction | ConfirmEmailAction;

export type EmailState = AxiosState<{
  email: string;
  emailVerified: boolean;
}>;
