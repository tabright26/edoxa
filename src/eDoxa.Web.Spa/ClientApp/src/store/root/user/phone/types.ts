import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Phone } from "types";

export const LOAD_USER_PHONE = "LOAD_USER_PHONE";
export const LOAD_USER_PHONE_SUCCESS = "LOAD_USER_PHONE_SUCCESS";
export const LOAD_USER_PHONE_FAIL = "LOAD_USER_PHONE_FAIL";

export const UPDATE_USER_PHONE = "UPDATE_USER_PHONE";
export const UPDATE_USER_PHONE_SUCCESS = "UPDATE_USER_PHONE_SUCCESS";
export const UPDATE_USER_PHONE_FAIL = "UPDATE_USER_PHONE_FAIL";

export type LoadUserPhoneType = typeof LOAD_USER_PHONE | typeof LOAD_USER_PHONE_SUCCESS | typeof LOAD_USER_PHONE_FAIL;
export type LoadUserPhoneActionCreator = AxiosActionCreator<LoadUserPhoneType>;
export type LoadUserPhoneAction = AxiosAction<LoadUserPhoneType>;

export type UpdateUserPhoneType = typeof UPDATE_USER_PHONE | typeof UPDATE_USER_PHONE_SUCCESS | typeof UPDATE_USER_PHONE_FAIL;
export type UpdateUserPhoneActionCreator = AxiosActionCreator<UpdateUserPhoneType>;
export type UpdateUserPhoneAction = AxiosAction<UpdateUserPhoneType>;

export type UserPhoneActionCreators = LoadUserPhoneActionCreator | UpdateUserPhoneActionCreator;
export type UserPhoneActions = LoadUserPhoneAction | UpdateUserPhoneAction;
export type UserPhoneState = AxiosState<Phone>;
