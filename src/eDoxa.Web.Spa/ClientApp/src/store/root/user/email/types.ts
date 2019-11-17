import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Email } from "types";

export const LOAD_USER_EMAIL = "LOAD_USER_EMAIL";
export const LOAD_USER_EMAIL_SUCCESS = "LOAD_USER_EMAIL_SUCCESS";
export const LOAD_USER_EMAIL_FAIL = "LOAD_USER_EMAIL_FAIL";

export const CONFIRM_USER_EMAIL = "CONFIRM_EMAIL";
export const CONFIRM_USER_EMAIL_SUCCESS = "CONFIRM_EMAIL_SUCCESS";
export const CONFIRM_USER_EMAIL_FAIL = "CONFIRM_EMAIL_FAIL";

export type LoadUserEmailType = typeof LOAD_USER_EMAIL | typeof LOAD_USER_EMAIL_SUCCESS | typeof LOAD_USER_EMAIL_FAIL;
export type LoadUserEmailActionCreator = AxiosActionCreator<LoadUserEmailType>;
export type LoadUserEmailAction = AxiosAction<LoadUserEmailType>;

export type ConfirmUserEmailType = typeof CONFIRM_USER_EMAIL | typeof CONFIRM_USER_EMAIL_SUCCESS | typeof CONFIRM_USER_EMAIL_FAIL;
export type ConfirmUserEmailActionCreator = AxiosActionCreator<ConfirmUserEmailType>;
export type ConfirmUserEmailAction = AxiosAction<ConfirmUserEmailType>;

export type UserEmailActionCreators = LoadUserEmailActionCreator | ConfirmUserEmailActionCreator;
export type UserEmailActions = LoadUserEmailAction | ConfirmUserEmailAction;
export type UserEmailState = AxiosState<Email>;
