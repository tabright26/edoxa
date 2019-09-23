import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_DOXATAG_HISTORY = "LOAD_DOXATAG_HISTORY";
export const LOAD_DOXATAG_HISTORY_SUCCESS = "LOAD_DOXATAG_HISTORY_SUCCESS";
export const LOAD_DOXATAG_HISTORY_FAIL = "LOAD_DOXATAG_HISTORY_FAIL";

export const CHANGE_DOXATAG = "CHANGE_DOXATAG";
export const CHANGE_DOXATAG_SUCCESS = "CHANGE_DOXATAG_SUCCESS";
export const CHANGE_DOXATAG_FAIL = "CHANGE_DOXATAG_FAIL";

type LoadDoxatagHistoryType = typeof LOAD_DOXATAG_HISTORY | typeof LOAD_DOXATAG_HISTORY_SUCCESS | typeof LOAD_DOXATAG_HISTORY_FAIL;

interface LoadDoxatagHistoryActionCreator extends AxiosActionCreator<LoadDoxatagHistoryType> {}

interface LoadDoxatagHistoryAction extends AxiosAction<LoadDoxatagHistoryType> {}

type ChangeDoxatagType = typeof CHANGE_DOXATAG | typeof CHANGE_DOXATAG_SUCCESS | typeof CHANGE_DOXATAG_FAIL;

interface ChangeDoxatagActionCreator extends AxiosActionCreator<ChangeDoxatagType> {}

interface ChangeDoxatagAction extends AxiosAction<ChangeDoxatagType> {}

export type DoxatagHistoryActionCreators = LoadDoxatagHistoryActionCreator | ChangeDoxatagActionCreator;

export type DoxatagHistoryActionTypes = LoadDoxatagHistoryAction | ChangeDoxatagAction;

export interface DoxatagHistoryState {}
