import { AxiosActionCreator, AxiosAction } from "interfaces/axios";

export const LOAD_ADDRESS_BOOK = "LOAD_ADDRESS_BOOK";
export const LOAD_ADDRESS_BOOK_SUCCESS = "LOAD_ADDRESS_BOOK_SUCCESS";
export const LOAD_ADDRESS_BOOK_FAIL = "LOAD_ADDRESS_BOOK_FAIL";

export const ADD_ADDRESS = "ADD_ADDRESS";
export const ADD_ADDRESS_SUCCESS = "ADD_ADDRESS_SUCCESS";
export const ADD_ADDRESS_FAIL = "ADD_ADDRESS_FAIL";

export const UPDATE_ADDRESS = "UPDATE_ADDRESS";
export const UPDATE_ADDRESS_SUCCESS = "UPDATE_ADDRESS_SUCCESS";
export const UPDATE_ADDRESS_FAIL = "UPDATE_ADDRESS_FAIL";

export const REMOVE_ADDRESS = "REMOVE_ADDRESS";
export const REMOVE_ADDRESS_SUCCESS = "REMOVE_ADDRESS_SUCCESS";
export const REMOVE_ADDRESS_FAIL = "REMOVE_ADDRESS_FAIL";

type LoadAddressBookType = typeof LOAD_ADDRESS_BOOK | typeof LOAD_ADDRESS_BOOK_SUCCESS | typeof LOAD_ADDRESS_BOOK_FAIL;

interface LoadAddressBookActionCreator extends AxiosActionCreator<LoadAddressBookType> {}

interface LoadAddressBookAction extends AxiosAction<LoadAddressBookType> {}

type AddAddressType = typeof ADD_ADDRESS | typeof ADD_ADDRESS_SUCCESS | typeof ADD_ADDRESS_FAIL;

interface AddAddressActionCreator extends AxiosActionCreator<AddAddressType> {}

interface AddAddressAction extends AxiosAction<AddAddressType> {}

type UpdateAddressType = typeof UPDATE_ADDRESS | typeof UPDATE_ADDRESS_SUCCESS | typeof UPDATE_ADDRESS_FAIL;

interface UpdateAddressActionCreator extends AxiosActionCreator<UpdateAddressType> {}

interface UpdateAddressAction extends AxiosAction<UpdateAddressType> {}

type RemoveAddressType = typeof REMOVE_ADDRESS | typeof REMOVE_ADDRESS_SUCCESS | typeof REMOVE_ADDRESS_FAIL;

interface RemoveAddressActionCreator extends AxiosActionCreator<RemoveAddressType> {}

interface RemoveAddressAction extends AxiosAction<RemoveAddressType> {}

export type AddressBookActionCreators = LoadAddressBookActionCreator | AddAddressActionCreator | UpdateAddressActionCreator | RemoveAddressActionCreator;

export type AddressBookActionTypes = LoadAddressBookAction | AddAddressAction | UpdateAddressAction | RemoveAddressAction;

export interface AddressBookState {}
