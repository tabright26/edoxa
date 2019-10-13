import { AxiosActionCreator, AxiosAction } from "store/types";

export const LOAD_PERSONAL_INFO = "LOAD_PERSONAL_INFO";
export const LOAD_PERSONAL_INFO_SUCCESS = "LOAD_PERSONAL_INFO_SUCCESS";
export const LOAD_PERSONAL_INFO_FAIL = "LOAD_PERSONAL_INFO_FAIL";

export const CREATE_PERSONAL_INFO = "CREATE_PERSONAL_INFO";
export const CREATE_PERSONAL_INFO_SUCCESS = "CREATE_PERSONAL_INFO_SUCCESS";
export const CREATE_PERSONAL_INFO_FAIL = "CREATE_PERSONAL_INFO_FAIL";

export const UPDATE_PERSONAL_INFO = "UPDATE_PERSONAL_INFO";
export const UPDATE_PERSONAL_INFO_SUCCESS = "UPDATE_PERSONAL_INFO_SUCCESS";
export const UPDATE_PERSONAL_INFO_FAIL = "UPDATE_PERSONAL_INFO_FAIL";

type LoadPersonalInfoType = typeof LOAD_PERSONAL_INFO | typeof LOAD_PERSONAL_INFO_SUCCESS | typeof LOAD_PERSONAL_INFO_FAIL;

interface LoadPersonalInfoActionCreator extends AxiosActionCreator<LoadPersonalInfoType> {}

interface LoadPersonalInfoAction extends AxiosAction<LoadPersonalInfoType> {}

type CreatePersonalInfoType = typeof CREATE_PERSONAL_INFO | typeof CREATE_PERSONAL_INFO_SUCCESS | typeof CREATE_PERSONAL_INFO_FAIL;

interface CreatePersonalInfoActionCreator extends AxiosActionCreator<CreatePersonalInfoType> {}

interface CreatePersonalInfoAction extends AxiosAction<CreatePersonalInfoType> {}

type UpdatePersonalInfoType = typeof UPDATE_PERSONAL_INFO | typeof UPDATE_PERSONAL_INFO_SUCCESS | typeof UPDATE_PERSONAL_INFO_FAIL;

interface UpdatePersonalInfoActionCreator extends AxiosActionCreator<UpdatePersonalInfoType> {}

interface UpdatePersonalInfoAction extends AxiosAction<UpdatePersonalInfoType> {}

export type PersonalInfoActionCreators = LoadPersonalInfoActionCreator | CreatePersonalInfoActionCreator | UpdatePersonalInfoActionCreator;

export type PersonalInfoActionTypes = LoadPersonalInfoAction | CreatePersonalInfoAction | UpdatePersonalInfoAction;

export interface PersonalInfoState {}
