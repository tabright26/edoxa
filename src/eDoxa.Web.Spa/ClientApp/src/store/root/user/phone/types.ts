import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";
import { Phone } from "types";

export const LOAD_USER_PHONE = "LOAD_USER_PHONE";
export const LOAD_USER_PHONE_SUCCESS = "LOAD_USER_PHONE_SUCCESS";
export const LOAD_USER_PHONE_FAIL = "LOAD_USER_PHONE_FAIL";

export const UPDATE_USER_PHONE = "UPDATE_USER_PHONE";
export const UPDATE_USER_PHONE_SUCCESS = "UPDATE_USER_PHONE_SUCCESS";
export const UPDATE_USER_PHONE_FAIL = "UPDATE_USER_PHONE_FAIL";

type LoadUserPhoneType = typeof LOAD_USER_PHONE | typeof LOAD_USER_PHONE_SUCCESS | typeof LOAD_USER_PHONE_FAIL;

interface LoadUserPhoneActionCreator extends AxiosActionCreator<LoadUserPhoneType> {}

interface LoadUserPhoneAction extends AxiosAction<LoadUserPhoneType> {}

type UpdateUserPhoneType = typeof UPDATE_USER_PHONE | typeof UPDATE_USER_PHONE_SUCCESS | typeof UPDATE_USER_PHONE_FAIL;

interface UpdateUserPhoneActionCreator extends AxiosActionCreator<UpdateUserPhoneType> {}

interface UpdateUserPhoneAction extends AxiosAction<UpdateUserPhoneType> {}

export type UserPhoneActionCreators = LoadUserPhoneActionCreator | UpdateUserPhoneActionCreator;

export type UserPhoneActions = LoadUserPhoneAction | UpdateUserPhoneAction;

export type UserPhoneState = AxiosState<Phone>;
