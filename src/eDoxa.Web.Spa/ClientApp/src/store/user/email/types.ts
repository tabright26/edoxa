import { AxiosActionCreator, AxiosAction } from "store/types";

export const CONFIRM_EMAIL = "CONFIRM_EMAIL";
export const CONFIRM_EMAIL_SUCCESS = "CONFIRM_EMAIL_SUCCESS";
export const CONFIRM_EMAIL_FAIL = "CONFIRM_EMAIL_FAIL";

type ConfirmEmailType = typeof CONFIRM_EMAIL | typeof CONFIRM_EMAIL_SUCCESS | typeof CONFIRM_EMAIL_FAIL;

interface ConfirmEmailActionCreator extends AxiosActionCreator<ConfirmEmailType> {}

interface ConfirmEmailAction extends AxiosAction<ConfirmEmailType> {}

export type EmailActionCreators = ConfirmEmailActionCreator;

export type EmailActionTypes = ConfirmEmailAction;

export interface EmailState {}
