import { AxiosActionCreator, AxiosAction } from "store/types";

export const LOAD_PHONENUMBER = "LOAD_PHONENUMBER";
export const LOAD_PHONENUMBER_SUCCESS = "LOAD_PHONENUMBER_SUCCESS";
export const LOAD_PHONENUMBER_FAIL = "LOAD_PHONENUMBER_FAIL";

type LoadPhoneNumberType = typeof LOAD_PHONENUMBER | typeof LOAD_PHONENUMBER_SUCCESS | typeof LOAD_PHONENUMBER_FAIL;

interface LoadPhoneNumberActionCreator extends AxiosActionCreator<LoadPhoneNumberType> {}

interface LoadPhoneNumberAction extends AxiosAction<LoadPhoneNumberType> {}

export type PhoneNumberActionCreators = LoadPhoneNumberActionCreator;

export type PhoneNumberActionTypes = LoadPhoneNumberAction;

export interface PhoneNumberState {
  phoneNumber: string;
  phoneNumberVerified: boolean;
}
