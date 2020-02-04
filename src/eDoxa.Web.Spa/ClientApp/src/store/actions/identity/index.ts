import publicIp from "public-ip";
import {
  LOAD_USER_ADDRESSBOOK,
  LOAD_USER_ADDRESSBOOK_SUCCESS,
  LOAD_USER_ADDRESSBOOK_FAIL,
  CREATE_USER_ADDRESS,
  CREATE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_FAIL,
  DELETE_USER_ADDRESS,
  DELETE_USER_ADDRESS_SUCCESS,
  DELETE_USER_ADDRESS_FAIL,
  UPDATE_USER_ADDRESS,
  UPDATE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_FAIL,
  LOAD_USER_DOXATAGHISTORY,
  LOAD_USER_DOXATAGHISTORY_SUCCESS,
  LOAD_USER_DOXATAGHISTORY_FAIL,
  CHANGE_USER_DOXATAG,
  CHANGE_USER_DOXATAG_SUCCESS,
  CHANGE_USER_DOXATAG_FAIL,
  LOAD_USER_PROFILE,
  LOAD_USER_PROFILE_SUCCESS,
  LOAD_USER_PROFILE_FAIL,
  CREATE_USER_PROFILE,
  CREATE_USER_PROFILE_SUCCESS,
  CREATE_USER_PROFILE_FAIL,
  UPDATE_USER_PROFILE,
  UPDATE_USER_PROFILE_SUCCESS,
  UPDATE_USER_PROFILE_FAIL,
  FORGOT_USER_PASSWORD,
  FORGOT_USER_PASSWORD_SUCCESS,
  FORGOT_USER_PASSWORD_FAIL,
  RESET_USER_PASSWORD,
  RESET_USER_PASSWORD_SUCCESS,
  RESET_USER_PASSWORD_FAIL,
  LOAD_USER_PHONE,
  LOAD_USER_PHONE_SUCCESS,
  LOAD_USER_PHONE_FAIL,
  UPDATE_USER_PHONE,
  UPDATE_USER_PHONE_SUCCESS,
  UPDATE_USER_PHONE_FAIL,
  LOAD_USER_EMAIL,
  LOAD_USER_EMAIL_SUCCESS,
  LOAD_USER_EMAIL_FAIL,
  CONFIRM_USER_EMAIL,
  CONFIRM_USER_EMAIL_SUCCESS,
  CONFIRM_USER_EMAIL_FAIL,
  LOGIN_USER_ACCOUNT,
  LOGIN_USER_ACCOUNT_SUCCESS,
  LOGIN_USER_ACCOUNT_FAIL,
  LOGOUT_USER_ACCOUNT,
  LOGOUT_USER_ACCOUNT_SUCCESS,
  LOGOUT_USER_ACCOUNT_FAIL,
  REGISTER_USER_ACCOUNT,
  REGISTER_USER_ACCOUNT_SUCCESS,
  REGISTER_USER_ACCOUNT_FAIL,
  RegisterUserAccountRequest,
  RegisterUserAccountActionCreator,
  LoginUserAccountActionCreator,
  LogoutUserAccountActionCreator,
  LoadUserAddressBookActionCreator,
  CreateUserAddressActionCreator,
  UpdateUserAddressActionCreator,
  DeleteUserAddressActionCreator,
  LoadUserDoxatagHistoryActionCreator,
  ChangeUserDoxatagActionCreator,
  LoadUserEmailActionCreator,
  ConfirmUserEmailActionCreator,
  LoadUserProfileActionCreator,
  CreateUserProfileActionCreator,
  UpdateUserProfileActionCreator,
  ForgotUserPasswordActionCreator,
  ResetUserPasswordActionCreator,
  LoadUserPhoneActionCreator,
  UpdateUserPhoneActionCreator
} from "./types";

import { AddressId } from "types";
import {
  AxiosActionCreatorMeta,
  AXIOS_PAYLOAD_CLIENT_DEFAULT,
  AXIOS_PAYLOAD_CLIENT_AUTHORITY
} from "utils/axios/types";
import { Dispatch } from "react";
import { RegisterUserAccountFormData } from "components/Account/Form/Register";

export function loadUserAddressBook(): LoadUserAddressBookActionCreator {
  return {
    types: [
      LOAD_USER_ADDRESSBOOK,
      LOAD_USER_ADDRESSBOOK_SUCCESS,
      LOAD_USER_ADDRESSBOOK_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `/identity/api/address-book`
      }
    }
  };
}

export function createUserAddress(
  data: any,
  meta: AxiosActionCreatorMeta
): CreateUserAddressActionCreator {
  return {
    types: [
      CREATE_USER_ADDRESS,
      CREATE_USER_ADDRESS_SUCCESS,
      CREATE_USER_ADDRESS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: "/identity/api/address-book",
        data
      }
    },
    meta
  };
}

export function updateUserAddress(
  addressId: AddressId,
  data: any,
  meta: AxiosActionCreatorMeta
): UpdateUserAddressActionCreator {
  return {
    types: [
      UPDATE_USER_ADDRESS,
      UPDATE_USER_ADDRESS_SUCCESS,
      UPDATE_USER_ADDRESS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "PUT",
        url: `/identity/api/address-book/${addressId}`,
        data
      }
    },
    meta
  };
}

export function deleteUserAddress(
  addressId: AddressId,
  meta: AxiosActionCreatorMeta
): DeleteUserAddressActionCreator {
  return {
    types: [
      DELETE_USER_ADDRESS,
      DELETE_USER_ADDRESS_SUCCESS,
      DELETE_USER_ADDRESS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "DELETE",
        url: `/identity/api/address-book/${addressId}`
      }
    },
    meta
  };
}

export function loadUserDoxatagHistory(): LoadUserDoxatagHistoryActionCreator {
  return {
    types: [
      LOAD_USER_DOXATAGHISTORY,
      LOAD_USER_DOXATAGHISTORY_SUCCESS,
      LOAD_USER_DOXATAGHISTORY_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: "/identity/api/doxatag-history"
      }
    }
  };
}

export function changeUserDoxatag(
  data: any,
  meta: AxiosActionCreatorMeta
): ChangeUserDoxatagActionCreator {
  return {
    types: [
      CHANGE_USER_DOXATAG,
      CHANGE_USER_DOXATAG_SUCCESS,
      CHANGE_USER_DOXATAG_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: "/identity/api/doxatag-history",
        data
      }
    },
    meta
  };
}

export function loadUserEmail(): LoadUserEmailActionCreator {
  return {
    types: [LOAD_USER_EMAIL, LOAD_USER_EMAIL_SUCCESS, LOAD_USER_EMAIL_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: "/identity/api/email"
      }
    }
  };
}

export function confirmUserEmail(
  userId: string,
  code: string
): ConfirmUserEmailActionCreator {
  return {
    types: [
      CONFIRM_USER_EMAIL,
      CONFIRM_USER_EMAIL_SUCCESS,
      CONFIRM_USER_EMAIL_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: "/identity/api/email/confirm",
        params: {
          userId,
          code
        }
      }
    }
  };
}

export function loadUserProfile(): LoadUserProfileActionCreator {
  return {
    types: [
      LOAD_USER_PROFILE,
      LOAD_USER_PROFILE_SUCCESS,
      LOAD_USER_PROFILE_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: "/identity/api/profile"
      }
    }
  };
}

export function createUserProfile(
  data: any,
  meta: AxiosActionCreatorMeta
): CreateUserProfileActionCreator {
  return {
    types: [
      CREATE_USER_PROFILE,
      CREATE_USER_PROFILE_SUCCESS,
      CREATE_USER_PROFILE_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: "/identity/api/profile",
        data
      }
    },
    meta
  };
}

export function updateUserProfile(
  data: any,
  meta: AxiosActionCreatorMeta
): UpdateUserProfileActionCreator {
  return {
    types: [
      UPDATE_USER_PROFILE,
      UPDATE_USER_PROFILE_SUCCESS,
      UPDATE_USER_PROFILE_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "PUT",
        url: "/identity/api/profile",
        data
      }
    },
    meta
  };
}

export function registerUserAccount(
  data: RegisterUserAccountFormData,
  meta: AxiosActionCreatorMeta
) {
  return async (dispatch: Dispatch<RegisterUserAccountActionCreator>) => {
    const request: RegisterUserAccountRequest = {
      email: data.email,
      password: data.password,
      country: data.countryIsoCode,
      dob: data.dob,
      ip: await publicIp.v4({
        fallbackUrls: ["https://ifconfig.co/ip"]
      })
    };
    const actionCreator: RegisterUserAccountActionCreator = {
      types: [
        REGISTER_USER_ACCOUNT,
        REGISTER_USER_ACCOUNT_SUCCESS,
        REGISTER_USER_ACCOUNT_FAIL
      ],
      payload: {
        client: AXIOS_PAYLOAD_CLIENT_AUTHORITY,
        request: {
          method: "POST",
          url: "/api/account/register",
          data: request,
          withCredentials: true
        }
      },
      meta
    };
    dispatch(actionCreator);
  };
}

export function loginUserAccount(
  data: any,
  meta: AxiosActionCreatorMeta
): LoginUserAccountActionCreator {
  return {
    types: [
      LOGIN_USER_ACCOUNT,
      LOGIN_USER_ACCOUNT_SUCCESS,
      LOGIN_USER_ACCOUNT_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_AUTHORITY,
      request: {
        method: "POST",
        url: "/api/account/login",
        data,
        withCredentials: true,
        responseType: "text"
      }
    },
    meta
  };
}

export function logoutUserAccount(
  logoutId: string | string[]
): LogoutUserAccountActionCreator {
  return {
    types: [
      LOGOUT_USER_ACCOUNT,
      LOGOUT_USER_ACCOUNT_SUCCESS,
      LOGOUT_USER_ACCOUNT_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_AUTHORITY,
      request: {
        method: "GET",
        url: "/api/account/logout",
        params: {
          logoutId
        },
        withCredentials: true
      }
    }
  };
}

export function forgotUserPassword(
  data: any,
  meta: AxiosActionCreatorMeta
): ForgotUserPasswordActionCreator {
  return {
    types: [
      FORGOT_USER_PASSWORD,
      FORGOT_USER_PASSWORD_SUCCESS,
      FORGOT_USER_PASSWORD_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: "/identity/api/password/forgot",
        data
      }
    },
    meta
  };
}

export function resetUserPassword(
  data: any,
  meta: AxiosActionCreatorMeta
): ResetUserPasswordActionCreator {
  return {
    types: [
      RESET_USER_PASSWORD,
      RESET_USER_PASSWORD_SUCCESS,
      RESET_USER_PASSWORD_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: "/identity/api/password/reset",
        data
      }
    },
    meta
  };
}

export function loadUserPhone(): LoadUserPhoneActionCreator {
  return {
    types: [LOAD_USER_PHONE, LOAD_USER_PHONE_SUCCESS, LOAD_USER_PHONE_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: "/identity/api/phone"
      }
    }
  };
}

export function updateUserPhone(
  data: any,
  meta: AxiosActionCreatorMeta
): UpdateUserPhoneActionCreator {
  return {
    types: [
      UPDATE_USER_PHONE,
      UPDATE_USER_PHONE_SUCCESS,
      UPDATE_USER_PHONE_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: "/identity/api/phone",
        data
      }
    },
    meta
  };
}
