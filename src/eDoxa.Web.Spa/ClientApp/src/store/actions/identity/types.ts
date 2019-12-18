import { AxiosActionCreator, AxiosAction } from "utils/axios/types";

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

export type LoadUserAddressBookType =
  | typeof LOAD_USER_ADDRESSBOOK
  | typeof LOAD_USER_ADDRESSBOOK_SUCCESS
  | typeof LOAD_USER_ADDRESSBOOK_FAIL;
export type LoadUserAddressBookActionCreator = AxiosActionCreator<
  LoadUserAddressBookType
>;
export type LoadUserAddressBookAction = AxiosAction<LoadUserAddressBookType>;

export type CreateUserAddressType =
  | typeof CREATE_USER_ADDRESS
  | typeof CREATE_USER_ADDRESS_SUCCESS
  | typeof CREATE_USER_ADDRESS_FAIL;
export type CreateUserAddressActionCreator = AxiosActionCreator<
  CreateUserAddressType
>;
export type CreateUserAddressAction = AxiosAction<CreateUserAddressType>;

export type UpdateUserAddressType =
  | typeof UPDATE_USER_ADDRESS
  | typeof UPDATE_USER_ADDRESS_SUCCESS
  | typeof UPDATE_USER_ADDRESS_FAIL;
export type UpdateUserAddressActionCreator = AxiosActionCreator<
  UpdateUserAddressType
>;
export type UpdateUserAddressAction = AxiosAction<UpdateUserAddressType>;

export type DeleteUserAddressType =
  | typeof DELETE_USER_ADDRESS
  | typeof DELETE_USER_ADDRESS_SUCCESS
  | typeof DELETE_USER_ADDRESS_FAIL;
export type DeleteUserAddressActionCreator = AxiosActionCreator<
  DeleteUserAddressType
>;
export type DeleteUserAddressAction = AxiosAction<DeleteUserAddressType>;

export type UserAddressBookActionCreators =
  | LoadUserAddressBookActionCreator
  | CreateUserAddressActionCreator
  | UpdateUserAddressActionCreator
  | DeleteUserAddressActionCreator;
export type UserAddressBookActions =
  | LoadUserAddressBookAction
  | CreateUserAddressAction
  | UpdateUserAddressAction
  | DeleteUserAddressAction;

export const LOAD_USER_DOXATAGHISTORY = "LOAD_USER_DOXATAGHISTORY";
export const LOAD_USER_DOXATAGHISTORY_SUCCESS =
  "LOAD_USER_DOXATAGHISTORY_SUCCESS";
export const LOAD_USER_DOXATAGHISTORY_FAIL = "LOAD_USER_DOXATAGHISTORY_FAIL";

export const UPDATE_USER_DOXATAG = "UPDATE_USER_DOXATAG";
export const UPDATE_USER_DOXATAG_SUCCESS = "UPDATE_USER_DOXATAG_SUCCESS";
export const UPDATE_USER_DOXATAG_FAIL = "UPDATE_USER_DOXATAG_FAIL";

export type LoadUserDoxatagHistoryType =
  | typeof LOAD_USER_DOXATAGHISTORY
  | typeof LOAD_USER_DOXATAGHISTORY_SUCCESS
  | typeof LOAD_USER_DOXATAGHISTORY_FAIL;
export type LoadUserDoxatagHistoryActionCreator = AxiosActionCreator<
  LoadUserDoxatagHistoryType
>;
export type LoadUserDoxatagHistoryAction = AxiosAction<
  LoadUserDoxatagHistoryType
>;

export type UpdateUserDoxatagType =
  | typeof UPDATE_USER_DOXATAG
  | typeof UPDATE_USER_DOXATAG_SUCCESS
  | typeof UPDATE_USER_DOXATAG_FAIL;
export type UpdateUserDoxatagActionCreator = AxiosActionCreator<
  UpdateUserDoxatagType
>;
export type UpdateUserDoxatagAction = AxiosAction<UpdateUserDoxatagType>;

export type UserDoxatagHistoryActionCreators =
  | LoadUserDoxatagHistoryActionCreator
  | UpdateUserDoxatagActionCreator;
export type UserDoxatagHistoryActions =
  | LoadUserDoxatagHistoryAction
  | UpdateUserDoxatagAction;

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
export type LoadUserEmailAction = AxiosAction<LoadUserEmailType>;

export type ConfirmUserEmailType =
  | typeof CONFIRM_USER_EMAIL
  | typeof CONFIRM_USER_EMAIL_SUCCESS
  | typeof CONFIRM_USER_EMAIL_FAIL;
export type ConfirmUserEmailActionCreator = AxiosActionCreator<
  ConfirmUserEmailType
>;
export type ConfirmUserEmailAction = AxiosAction<ConfirmUserEmailType>;

export type UserEmailActionCreators =
  | LoadUserEmailActionCreator
  | ConfirmUserEmailActionCreator;
export type UserEmailActions = LoadUserEmailAction | ConfirmUserEmailAction;

export const LOAD_USER_INFORMATIONS = "LOAD_USER_INFORMATIONS";
export const LOAD_USER_INFORMATIONS_SUCCESS = "LOAD_USER_INFORMATIONS_SUCCESS";
export const LOAD_USER_INFORMATIONS_FAIL = "LOAD_USER_INFORMATIONS_FAIL";

export const CREATE_USER_INFORMATIONS = "CREATE_USER_INFORMATIONS";
export const CREATE_USER_INFORMATIONS_SUCCESS =
  "CREATE_USER_INFORMATIONS_SUCCESS";
export const CREATE_USER_INFORMATIONS_FAIL = "CREATE_USER_INFORMATIONS_FAIL";

export const UPDATE_USER_INFORMATIONS = "UPDATE_USER_INFORMATIONS";
export const UPDATE_USER_INFORMATIONS_SUCCESS =
  "UPDATE_USER_INFORMATIONS_SUCCESS";
export const UPDATE_USER_INFORMATIONS_FAIL = "UPDATE_USER_INFORMATIONS_FAIL";

export type LoadUserInformationsType =
  | typeof LOAD_USER_INFORMATIONS
  | typeof LOAD_USER_INFORMATIONS_SUCCESS
  | typeof LOAD_USER_INFORMATIONS_FAIL;
export type LoadUserInformationsActionCreator = AxiosActionCreator<
  LoadUserInformationsType
>;
export type LoadUserInformationsAction = AxiosAction<LoadUserInformationsType>;

export type CreateUserInformationsType =
  | typeof CREATE_USER_INFORMATIONS
  | typeof CREATE_USER_INFORMATIONS_SUCCESS
  | typeof CREATE_USER_INFORMATIONS_FAIL;
export type CreateUserInformationsActionCreator = AxiosActionCreator<
  CreateUserInformationsType
>;
export type CreateUserInformationsAction = AxiosAction<
  CreateUserInformationsType
>;

export type UpdateUserInformationsType =
  | typeof UPDATE_USER_INFORMATIONS
  | typeof UPDATE_USER_INFORMATIONS_SUCCESS
  | typeof UPDATE_USER_INFORMATIONS_FAIL;
export type UpdateUserInformationsActionCreator = AxiosActionCreator<
  UpdateUserInformationsType
>;
export type UpdateUserInformationsAction = AxiosAction<
  UpdateUserInformationsType
>;

export type UserInformationsActionCreators =
  | LoadUserInformationsActionCreator
  | CreateUserInformationsActionCreator
  | UpdateUserInformationsActionCreator;
export type UserInformationsActions =
  | LoadUserInformationsAction
  | CreateUserInformationsAction
  | UpdateUserInformationsAction;

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

export type UserPasswordActionCreators =
  | ForgotUserPasswordActionCreator
  | ResetUserPasswordActionCreator;
export type UserPasswordActions =
  | ForgotUserPasswordAction
  | ResetUserPasswordAction;

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

export type UserPhoneActionCreators =
  | LoadUserPhoneActionCreator
  | UpdateUserPhoneActionCreator;
export type UserPhoneActions = LoadUserPhoneAction | UpdateUserPhoneAction;
