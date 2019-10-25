import { AxiosActionCreator, AxiosAction, AxiosState } from "utils/axios/types";
import { Address } from "types";

export const LOAD_USER_ADDRESSBOOK = "LOAD_USER_ADDRESSBOOK";
export const LOAD_USER_ADDRESSBOOK_SUCCESS = "LOAD_USER_ADDRESSBOOK_SUCCESS";
export const LOAD_USER_ADDRESSBOOK_FAIL = "LOAD_USER_ADDRESSBOOK_FAIL";

export const CREATE_USER_ADDRESS = "CREATE_USER_ADDRESS";
export const CREATE_USER_ADDRESS_SUCCESS = "CREATE_USER_ADDRESS_SUCCESS";
export const CREATE_USER_ADDRESS_FAIL = "CREATE_USER_ADDRESS_FAIL";

export const UPDATE_USER_ADDRESS = "UPDATE_USER_ADDRESS";
export const UPDATE_USER_ADDRESS_SUCCESS = "UPDATE_USER_ADDRESS_SUCCESS";
export const UPDATE_USER_ADDRESS_FAIL = "UPDATE_USER_ADDRESS_FAIL";

export const DELETE_USER_ADDRESS = "DELETE_USER_ADDRESS";
export const DELETE_USER_ADDRESS_SUCCESS = "DELETE_USER_ADDRESS_SUCCESS";
export const DELETE_USER_ADDRESS_FAIL = "DELETE_USER_ADDRESS_FAIL";

export type LoadUserAddressBookType = typeof LOAD_USER_ADDRESSBOOK | typeof LOAD_USER_ADDRESSBOOK_SUCCESS | typeof LOAD_USER_ADDRESSBOOK_FAIL;
export type LoadUserAddressBookActionCreator = AxiosActionCreator<LoadUserAddressBookType>;
export type LoadUserAddressBookAction = AxiosAction<LoadUserAddressBookType>;

export type CreateUserAddressType = typeof CREATE_USER_ADDRESS | typeof CREATE_USER_ADDRESS_SUCCESS | typeof CREATE_USER_ADDRESS_FAIL;
export type CreateUserAddressActionCreator = AxiosActionCreator<CreateUserAddressType>;
export type CreateUserAddressAction = AxiosAction<CreateUserAddressType>;

export type UpdateUserAddressType = typeof UPDATE_USER_ADDRESS | typeof UPDATE_USER_ADDRESS_SUCCESS | typeof UPDATE_USER_ADDRESS_FAIL;
export type UpdateUserAddressActionCreator = AxiosActionCreator<UpdateUserAddressType>;
export type UpdateUserAddressAction = AxiosAction<UpdateUserAddressType>;

export type DeleteUserAddressType = typeof DELETE_USER_ADDRESS | typeof DELETE_USER_ADDRESS_SUCCESS | typeof DELETE_USER_ADDRESS_FAIL;
export type DeleteUserAddressActionCreator = AxiosActionCreator<DeleteUserAddressType>;
export type DeleteUserAddressAction = AxiosAction<DeleteUserAddressType>;

export type UserAddressBookActionCreators = LoadUserAddressBookActionCreator | CreateUserAddressActionCreator | UpdateUserAddressActionCreator | DeleteUserAddressActionCreator;
export type UserAddressBookActions = LoadUserAddressBookAction | CreateUserAddressAction | UpdateUserAddressAction | DeleteUserAddressAction;
export type UserAddressBookState = AxiosState<Address[]>;
