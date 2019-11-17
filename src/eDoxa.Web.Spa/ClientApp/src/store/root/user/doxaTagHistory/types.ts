import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Doxatag } from "types";

export const LOAD_USER_DOXATAGHISTORY = "LOAD_USER_DOXATAGHISTORY";
export const LOAD_USER_DOXATAGHISTORY_SUCCESS = "LOAD_USER_DOXATAGHISTORY_SUCCESS";
export const LOAD_USER_DOXATAGHISTORY_FAIL = "LOAD_USER_DOXATAGHISTORY_FAIL";

export const UPDATE_USER_DOXATAG = "UPDATE_USER_DOXATAG";
export const UPDATE_USER_DOXATAG_SUCCESS = "UPDATE_USER_DOXATAG_SUCCESS";
export const UPDATE_USER_DOXATAG_FAIL = "UPDATE_USER_DOXATAG_FAIL";

export type LoadUserDoxatagHistoryType = typeof LOAD_USER_DOXATAGHISTORY | typeof LOAD_USER_DOXATAGHISTORY_SUCCESS | typeof LOAD_USER_DOXATAGHISTORY_FAIL;
export type LoadUserDoxatagHistoryActionCreator = AxiosActionCreator<LoadUserDoxatagHistoryType>;
export type LoadUserDoxatagHistoryAction = AxiosAction<LoadUserDoxatagHistoryType>;

export type UpdateUserDoxatagType = typeof UPDATE_USER_DOXATAG | typeof UPDATE_USER_DOXATAG_SUCCESS | typeof UPDATE_USER_DOXATAG_FAIL;
export type UpdateUserDoxatagActionCreator = AxiosActionCreator<UpdateUserDoxatagType>;
export type UpdateUserDoxatagAction = AxiosAction<UpdateUserDoxatagType>;

export type UserDoxatagHistoryActionCreators = LoadUserDoxatagHistoryActionCreator | UpdateUserDoxatagActionCreator;
export type UserDoxatagHistoryActions = LoadUserDoxatagHistoryAction | UpdateUserDoxatagAction;
export type UserDoxatagHistoryState = AxiosState<Doxatag[]>;
