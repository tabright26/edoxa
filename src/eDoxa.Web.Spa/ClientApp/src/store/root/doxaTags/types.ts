import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Doxatag } from "types";

export const LOAD_DOXATAGS = "LOAD_DOXATAGS";
export const LOAD_DOXATAGS_SUCCESS = "LOAD_DOXATAGS_SUCCESS";
export const LOAD_DOXATAGS_FAIL = "LOAD_DOXATAGS_FAIL";

export type LoadDoxatagsType = typeof LOAD_DOXATAGS | typeof LOAD_DOXATAGS_SUCCESS | typeof LOAD_DOXATAGS_FAIL;
export type LoadDoxatagsActionCreator = AxiosActionCreator<LoadDoxatagsType>;
export type LoadDoxatagsAction = AxiosAction<LoadDoxatagsType>;

export type DoxatagsActionCreators = LoadDoxatagsActionCreator;
export type DoxatagsActions = LoadDoxatagsAction;
export type DoxatagsState = AxiosState<Doxatag[]>;