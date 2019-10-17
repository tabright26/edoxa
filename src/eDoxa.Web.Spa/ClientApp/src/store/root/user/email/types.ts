import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";
import { Email } from "types";

export const LOAD_USER_EMAIL = "LOAD_USER_EMAIL";
export const LOAD_USER_EMAIL_SUCCESS = "LOAD_USER_EMAIL_SUCCESS";
export const LOAD_USER_EMAIL_FAIL = "LOAD_USER_EMAIL_FAIL";

export const CONFIRM_USER_EMAIL = "CONFIRM_EMAIL";
export const CONFIRM_USER_EMAIL_SUCCESS = "CONFIRM_EMAIL_SUCCESS";
export const CONFIRM_USER_EMAIL_FAIL = "CONFIRM_EMAIL_FAIL";

type LoadUserEmailType = typeof LOAD_USER_EMAIL | typeof LOAD_USER_EMAIL_SUCCESS | typeof LOAD_USER_EMAIL_FAIL;

interface LoadUserEmailActionCreator extends AxiosActionCreator<LoadUserEmailType> {}

interface LoadUserEmailAction extends AxiosAction<LoadUserEmailType> {}

type ConfirmUserEmailType = typeof CONFIRM_USER_EMAIL | typeof CONFIRM_USER_EMAIL_SUCCESS | typeof CONFIRM_USER_EMAIL_FAIL;

interface ConfirmUserEmailActionCreator extends AxiosActionCreator<ConfirmUserEmailType> {}

interface ConfirmUserEmailAction extends AxiosAction<ConfirmUserEmailType> {}

export type UserEmailActionCreators = LoadUserEmailActionCreator | ConfirmUserEmailActionCreator;

export type UserEmailActions = LoadUserEmailAction | ConfirmUserEmailAction;

export type UserEmailState = AxiosState<Email>;
