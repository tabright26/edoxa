import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_DOXATAGS = "LOAD_DOXATAGS";
export const LOAD_DOXATAGS_SUCCESS = "LOAD_DOXATAGS_SUCCESS";
export const LOAD_DOXATAGS_FAIL = "LOAD_DOXATAGS_FAIL";

type LoadDoxatagsType = typeof LOAD_DOXATAGS | typeof LOAD_DOXATAGS_SUCCESS | typeof LOAD_DOXATAGS_FAIL;

interface LoadDoxatagsActionCreator extends AxiosActionCreator<LoadDoxatagsType> {}

interface LoadDoxatagsAction extends AxiosAction<LoadDoxatagsType> {}

export type DoxatagsActionCreators = LoadDoxatagsActionCreator;

export type DoxatagsActionTypes = LoadDoxatagsAction;

export interface DoxatagsState {}