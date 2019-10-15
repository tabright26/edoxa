import { AxiosActionCreator, AxiosAction, AxiosState } from "store/middlewares/axios/types";

export const LOAD_PHONENUMBER = "LOAD_PHONENUMBER";
export const LOAD_PHONENUMBER_SUCCESS = "LOAD_PHONENUMBER_SUCCESS";
export const LOAD_PHONENUMBER_FAIL = "LOAD_PHONENUMBER_FAIL";

export const CHANGE_PHONENUMBER = "CHANGE_PHONENUMBER";
export const CHANGE_PHONENUMBER_SUCCESS = "CHANGE_PHONENUMBER_SUCCESS";
export const CHANGE_PHONENUMBER_FAIL = "CHANGE_PHONENUMBER_FAIL";

type LoadPhoneNumberType = typeof LOAD_PHONENUMBER | typeof LOAD_PHONENUMBER_SUCCESS | typeof LOAD_PHONENUMBER_FAIL;

interface LoadPhoneNumberActionCreator extends AxiosActionCreator<LoadPhoneNumberType> {}

interface LoadPhoneNumberAction extends AxiosAction<LoadPhoneNumberType> {}

type ChangePhoneNumberType = typeof CHANGE_PHONENUMBER | typeof CHANGE_PHONENUMBER_SUCCESS | typeof CHANGE_PHONENUMBER_FAIL;

interface ChangePhoneNumberActionCreator extends AxiosActionCreator<ChangePhoneNumberType> {}

interface ChangePhoneNumberAction extends AxiosAction<ChangePhoneNumberType> {}

export type PhoneNumberActionCreators = LoadPhoneNumberActionCreator | ChangePhoneNumberActionCreator;

export type PhoneNumberActionTypes = LoadPhoneNumberAction | ChangePhoneNumberAction;

export type PhoneNumberState = AxiosState<{
  phoneNumber: string;
  phoneNumberVerified: boolean;
}>;
