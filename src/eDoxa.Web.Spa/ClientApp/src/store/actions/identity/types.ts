import { AxiosActionCreator, AxiosAction } from "utils/axios/types";
import {
  UserAddress,
  UserDoxatag,
  UserEmail,
  UserProfile,
  LogoutToken
} from "types";

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

export const LOGIN_USER_ACCOUNT = "LOGIN_USER_ACCOUNT";
export const LOGIN_USER_ACCOUNT_SUCCESS = "LOGIN_USER_ACCOUNT_SUCCESS";
export const LOGIN_USER_ACCOUNT_FAIL = "LOGIN_USER_ACCOUNT_FAIL";

export const LOGOUT_USER_ACCOUNT = "LOGOUT_USER_ACCOUNT";
export const LOGOUT_USER_ACCOUNT_SUCCESS = "LOGOUT_USER_ACCOUNT_SUCCESS";
export const LOGOUT_USER_ACCOUNT_FAIL = "LOGOUT_USER_ACCOUNT_FAIL";

export const REGISTER_USER_ACCOUNT = "REGISTER_USER_ACCOUNT";
export const REGISTER_USER_ACCOUNT_SUCCESS = "REGISTER_USER_ACCOUNT_SUCCESS";
export const REGISTER_USER_ACCOUNT_FAIL = "REGISTER_USER_ACCOUNT_FAIL";

export type RegisterUserAccountType =
  | typeof REGISTER_USER_ACCOUNT
  | typeof REGISTER_USER_ACCOUNT_SUCCESS
  | typeof REGISTER_USER_ACCOUNT_FAIL;
export type RegisterUserAccountActionCreator = AxiosActionCreator<
  RegisterUserAccountType
>;
export type RegisterUserAccountAction = AxiosAction<RegisterUserAccountType>;
export type RegisterUserAccountRequest = {
  email: string;
  password: string;
  country: string;
  ip: string;
  dob: string;
};

export type LoginUserAccountType =
  | typeof LOGIN_USER_ACCOUNT
  | typeof LOGIN_USER_ACCOUNT_SUCCESS
  | typeof LOGIN_USER_ACCOUNT_FAIL;
export type LoginUserAccountActionCreator = AxiosActionCreator<
  LoginUserAccountType
>;
export type LoginUserAccountAction = AxiosAction<LoginUserAccountType, string>;

export type LogoutUserAccountType =
  | typeof LOGOUT_USER_ACCOUNT
  | typeof LOGOUT_USER_ACCOUNT_SUCCESS
  | typeof LOGOUT_USER_ACCOUNT_FAIL;
export type LogoutUserAccountActionCreator = AxiosActionCreator<
  LogoutUserAccountType
>;
export type LogoutUserAccountAction = AxiosAction<
  LogoutUserAccountType,
  LogoutToken
>;

export type LoadUserAddressBookType =
  | typeof LOAD_USER_ADDRESSBOOK
  | typeof LOAD_USER_ADDRESSBOOK_SUCCESS
  | typeof LOAD_USER_ADDRESSBOOK_FAIL;
export type LoadUserAddressBookActionCreator = AxiosActionCreator<
  LoadUserAddressBookType
>;
export type LoadUserAddressBookAction = AxiosAction<
  LoadUserAddressBookType,
  UserAddress[]
>;

export type CreateUserAddressType =
  | typeof CREATE_USER_ADDRESS
  | typeof CREATE_USER_ADDRESS_SUCCESS
  | typeof CREATE_USER_ADDRESS_FAIL;
export type CreateUserAddressActionCreator = AxiosActionCreator<
  CreateUserAddressType
>;
export type CreateUserAddressAction = AxiosAction<
  CreateUserAddressType,
  UserAddress
>;

export type UpdateUserAddressType =
  | typeof UPDATE_USER_ADDRESS
  | typeof UPDATE_USER_ADDRESS_SUCCESS
  | typeof UPDATE_USER_ADDRESS_FAIL;
export type UpdateUserAddressActionCreator = AxiosActionCreator<
  UpdateUserAddressType
>;
export type UpdateUserAddressAction = AxiosAction<
  UpdateUserAddressType,
  UserAddress
>;

export type DeleteUserAddressType =
  | typeof DELETE_USER_ADDRESS
  | typeof DELETE_USER_ADDRESS_SUCCESS
  | typeof DELETE_USER_ADDRESS_FAIL;
export type DeleteUserAddressActionCreator = AxiosActionCreator<
  DeleteUserAddressType
>;
export type DeleteUserAddressAction = AxiosAction<
  DeleteUserAddressType,
  UserAddress
>;

export const LOAD_USER_DOXATAGHISTORY = "LOAD_USER_DOXATAGHISTORY";
export const LOAD_USER_DOXATAGHISTORY_SUCCESS =
  "LOAD_USER_DOXATAGHISTORY_SUCCESS";
export const LOAD_USER_DOXATAGHISTORY_FAIL = "LOAD_USER_DOXATAGHISTORY_FAIL";

export const CHANGE_USER_DOXATAG = "CHANGE_USER_DOXATAG";
export const CHANGE_USER_DOXATAG_SUCCESS = "CHANGE_USER_DOXATAG_SUCCESS";
export const CHANGE_USER_DOXATAG_FAIL = "CHANGE_USER_DOXATAG_FAIL";

export type LoadUserDoxatagHistoryType =
  | typeof LOAD_USER_DOXATAGHISTORY
  | typeof LOAD_USER_DOXATAGHISTORY_SUCCESS
  | typeof LOAD_USER_DOXATAGHISTORY_FAIL;
export type LoadUserDoxatagHistoryActionCreator = AxiosActionCreator<
  LoadUserDoxatagHistoryType
>;
export type LoadUserDoxatagHistoryAction = AxiosAction<
  LoadUserDoxatagHistoryType,
  UserDoxatag[]
>;

export type ChangeUserDoxatagType =
  | typeof CHANGE_USER_DOXATAG
  | typeof CHANGE_USER_DOXATAG_SUCCESS
  | typeof CHANGE_USER_DOXATAG_FAIL;
export type ChangeUserDoxatagActionCreator = AxiosActionCreator<
  ChangeUserDoxatagType
>;
export type ChangeUserDoxatagAction = AxiosAction<
  ChangeUserDoxatagType,
  UserDoxatag
>;

export const LOAD_USER_EMAIL = "LOAD_USER_EMAIL";
export const LOAD_USER_EMAIL_SUCCESS = "LOAD_USER_EMAIL_SUCCESS";
export const LOAD_USER_EMAIL_FAIL = "LOAD_USER_EMAIL_FAIL";

export const CONFIRM_USER_EMAIL = "CONFIRM_EMAIL";
export const CONFIRM_USER_EMAIL_SUCCESS = "CONFIRM_EMAIL_SUCCESS";
export const CONFIRM_USER_EMAIL_FAIL = "CONFIRM_EMAIL_FAIL";

export type LoadUserEmailType =
  | typeof LOAD_USER_EMAIL
  | typeof LOAD_USER_EMAIL_SUCCESS
  | typeof LOAD_USER_EMAIL_FAIL;
export type LoadUserEmailActionCreator = AxiosActionCreator<LoadUserEmailType>;
export type LoadUserEmailAction = AxiosAction<LoadUserEmailType, UserEmail>;

export type ConfirmUserEmailType =
  | typeof CONFIRM_USER_EMAIL
  | typeof CONFIRM_USER_EMAIL_SUCCESS
  | typeof CONFIRM_USER_EMAIL_FAIL;
export type ConfirmUserEmailActionCreator = AxiosActionCreator<
  ConfirmUserEmailType
>;
export type ConfirmUserEmailAction = AxiosAction<
  ConfirmUserEmailType,
  UserEmail
>;

export const LOAD_USER_PROFILE = "LOAD_USER_PROFILE";
export const LOAD_USER_PROFILE_SUCCESS = "LOAD_USER_PROFILE_SUCCESS";
export const LOAD_USER_PROFILE_FAIL = "LOAD_USER_PROFILE_FAIL";

export const CREATE_USER_PROFILE = "CREATE_USER_PROFILE";
export const CREATE_USER_PROFILE_SUCCESS = "CREATE_USER_PROFILE_SUCCESS";
export const CREATE_USER_PROFILE_FAIL = "CREATE_USER_PROFILE_FAIL";

export const UPDATE_USER_PROFILE = "UPDATE_USER_PROFILE";
export const UPDATE_USER_PROFILE_SUCCESS = "UPDATE_USER_PROFILE_SUCCESS";
export const UPDATE_USER_PROFILE_FAIL = "UPDATE_USER_PROFILE_FAIL";

export type LoadUserProfileType =
  | typeof LOAD_USER_PROFILE
  | typeof LOAD_USER_PROFILE_SUCCESS
  | typeof LOAD_USER_PROFILE_FAIL;
export type LoadUserProfileActionCreator = AxiosActionCreator<
  LoadUserProfileType
>;
export type LoadUserProfileAction = AxiosAction<
  LoadUserProfileType,
  UserProfile
>;

export type CreateUserProfileType =
  | typeof CREATE_USER_PROFILE
  | typeof CREATE_USER_PROFILE_SUCCESS
  | typeof CREATE_USER_PROFILE_FAIL;
export type CreateUserProfileActionCreator = AxiosActionCreator<
  CreateUserProfileType
>;
export type CreateUserProfileAction = AxiosAction<
  CreateUserProfileType,
  UserProfile
>;

export type UpdateUserProfileType =
  | typeof UPDATE_USER_PROFILE
  | typeof UPDATE_USER_PROFILE_SUCCESS
  | typeof UPDATE_USER_PROFILE_FAIL;
export type UpdateUserProfileActionCreator = AxiosActionCreator<
  UpdateUserProfileType
>;
export type UpdateUserProfileAction = AxiosAction<
  UpdateUserProfileType,
  UserProfile
>;

export const FORGOT_USER_PASSWORD = "FORGOT_USER_PASSWORD";
export const FORGOT_USER_PASSWORD_SUCCESS = "FORGOT_USER_PASSWORD_SUCCESS";
export const FORGOT_USER_PASSWORD_FAIL = "FORGOT_USER_PASSWORD_FAIL";

export const RESET_USER_PASSWORD = "RESET_USER_PASSWORD";
export const RESET_USER_PASSWORD_SUCCESS = "RESET_USER_PASSWORD_SUCCESS";
export const RESET_USER_PASSWORD_FAIL = "RESET_USER_PASSWORD_FAIL";

export type ForgotUserPasswordType =
  | typeof FORGOT_USER_PASSWORD
  | typeof FORGOT_USER_PASSWORD_SUCCESS
  | typeof FORGOT_USER_PASSWORD_FAIL;
export type ForgotUserPasswordActionCreator = AxiosActionCreator<
  ForgotUserPasswordType
>;
export type ForgotUserPasswordAction = AxiosAction<ForgotUserPasswordType>;

export type ResetUserPasswordType =
  | typeof RESET_USER_PASSWORD
  | typeof RESET_USER_PASSWORD_SUCCESS
  | typeof RESET_USER_PASSWORD_FAIL;
export type ResetUserPasswordActionCreator = AxiosActionCreator<
  ResetUserPasswordType
>;
export type ResetUserPasswordAction = AxiosAction<ResetUserPasswordType>;

export const LOAD_USER_PHONE = "LOAD_USER_PHONE";
export const LOAD_USER_PHONE_SUCCESS = "LOAD_USER_PHONE_SUCCESS";
export const LOAD_USER_PHONE_FAIL = "LOAD_USER_PHONE_FAIL";

export const UPDATE_USER_PHONE = "UPDATE_USER_PHONE";
export const UPDATE_USER_PHONE_SUCCESS = "UPDATE_USER_PHONE_SUCCESS";
export const UPDATE_USER_PHONE_FAIL = "UPDATE_USER_PHONE_FAIL";

export type LoadUserPhoneType =
  | typeof LOAD_USER_PHONE
  | typeof LOAD_USER_PHONE_SUCCESS
  | typeof LOAD_USER_PHONE_FAIL;
export type LoadUserPhoneActionCreator = AxiosActionCreator<LoadUserPhoneType>;
export type LoadUserPhoneAction = AxiosAction<LoadUserPhoneType>;
export type UpdateUserPhoneType =
  | typeof UPDATE_USER_PHONE
  | typeof UPDATE_USER_PHONE_SUCCESS
  | typeof UPDATE_USER_PHONE_FAIL;
export type UpdateUserPhoneActionCreator = AxiosActionCreator<
  UpdateUserPhoneType
>;
export type UpdateUserPhoneAction = AxiosAction<UpdateUserPhoneType>;
