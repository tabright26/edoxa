import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";
import { Informations } from "types";

export const LOAD_USER_INFORMATIONS = "LOAD_USER_INFORMATIONS";
export const LOAD_USER_INFORMATIONS_SUCCESS = "LOAD_USER_INFORMATIONS_SUCCESS";
export const LOAD_USER_INFORMATIONS_FAIL = "LOAD_USER_INFORMATIONS_FAIL";

export const CREATE_USER_INFORMATIONS = "CREATE_USER_INFORMATIONS";
export const CREATE_USER_INFORMATIONS_SUCCESS = "CREATE_USER_INFORMATIONS_SUCCESS";
export const CREATE_USER_INFORMATIONS_FAIL = "CREATE_USER_INFORMATIONS_FAIL";

export const UPDATE_USER_INFORMATIONS = "UPDATE_USER_INFORMATIONS";
export const UPDATE_USER_INFORMATIONS_SUCCESS = "UPDATE_USER_INFORMATIONS_SUCCESS";
export const UPDATE_USER_INFORMATIONS_FAIL = "UPDATE_USER_INFORMATIONS_FAIL";

type LoadUserInformationsType = typeof LOAD_USER_INFORMATIONS | typeof LOAD_USER_INFORMATIONS_SUCCESS | typeof LOAD_USER_INFORMATIONS_FAIL;

interface LoadUserInformationsActionCreator extends AxiosActionCreator<LoadUserInformationsType> {}

interface LoadUserInformationsAction extends AxiosAction<LoadUserInformationsType> {}

type CreateUserInformationsType = typeof CREATE_USER_INFORMATIONS | typeof CREATE_USER_INFORMATIONS_SUCCESS | typeof CREATE_USER_INFORMATIONS_FAIL;

interface CreateUserInformationsActionCreator extends AxiosActionCreator<CreateUserInformationsType> {}

interface CreateUserInformationsAction extends AxiosAction<CreateUserInformationsType> {}

type UpdateUserInformationsType = typeof UPDATE_USER_INFORMATIONS | typeof UPDATE_USER_INFORMATIONS_SUCCESS | typeof UPDATE_USER_INFORMATIONS_FAIL;

interface UpdateUserInformationsActionCreator extends AxiosActionCreator<UpdateUserInformationsType> {}

interface UpdateUserInformationsAction extends AxiosAction<UpdateUserInformationsType> {}

export type UserInformationsActionCreators = LoadUserInformationsActionCreator | CreateUserInformationsActionCreator | UpdateUserInformationsActionCreator;

export type UserInformationsActions = LoadUserInformationsAction | CreateUserInformationsAction | UpdateUserInformationsAction;

export type UserInformationsState = AxiosState<Informations>;
