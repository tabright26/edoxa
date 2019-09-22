import { IAxiosActionCreator } from "interfaces/axios";

export const LOAD_DOXATAGS = "LOAD_DOXATAGS";
export const LOAD_DOXATAGS_SUCCESS = "LOAD_DOXATAGS_SUCCESS";
export const LOAD_DOXATAGS_FAIL = "LOAD_DOXATAGS_FAIL";
export type LoadDoxaTagsActionType = "LOAD_DOXATAGS" | "LOAD_DOXATAGS_SUCCESS" | "LOAD_DOXATAGS_FAIL";
export function loadDoxaTags(): IAxiosActionCreator<LoadDoxaTagsActionType> {
  return {
    types: ["LOAD_DOXATAGS", "LOAD_DOXATAGS_SUCCESS", "LOAD_DOXATAGS_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/doxatags"
      }
    }
  };
}

export const LOAD_DOXATAG_HISTORY = "LOAD_DOXATAG_HISTORY";
export const LOAD_DOXATAG_HISTORY_SUCCESS = "LOAD_DOXATAG_HISTORY_SUCCESS";
export const LOAD_DOXATAG_HISTORY_FAIL = "LOAD_DOXATAG_HISTORY_FAIL";
export type LoadDoxaTagHistoryActionType = "LOAD_DOXATAG_HISTORY" | "LOAD_DOXATAG_HISTORY_SUCCESS" | "LOAD_DOXATAG_HISTORY_FAIL";
export function loadDoxaTagHistory(): IAxiosActionCreator<LoadDoxaTagHistoryActionType> {
  return {
    types: ["LOAD_DOXATAG_HISTORY", "LOAD_DOXATAG_HISTORY_SUCCESS", "LOAD_DOXATAG_HISTORY_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/doxatag-history"
      }
    }
  };
}

export const CHANGE_DOXATAG = "CHANGE_DOXATAG";
export const CHANGE_DOXATAG_SUCCESS = "CHANGE_DOXATAG_SUCCESS";
export const CHANGE_DOXATAG_FAIL = "CHANGE_DOXATAG_FAIL";
export type ChangeDoxaTagActionType = "CHANGE_DOXATAG" | "CHANGE_DOXATAG_SUCCESS" | "CHANGE_DOXATAG_FAIL";
export function changeDoxaTag(data: any): IAxiosActionCreator<ChangeDoxaTagActionType> {
  return {
    types: ["CHANGE_DOXATAG", "CHANGE_DOXATAG_SUCCESS", "CHANGE_DOXATAG_FAIL"],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/doxatag-history",
        data
      }
    }
  };
}

export const LOAD_PERSONAL_INFO = "LOAD_PERSONAL_INFO";
export const LOAD_PERSONAL_INFO_SUCCESS = "LOAD_PERSONAL_INFO_SUCCESS";
export const LOAD_PERSONAL_INFO_FAIL = "LOAD_PERSONAL_INFO_FAIL";
export type LoadPersonalInfoActionType = "LOAD_PERSONAL_INFO" | "LOAD_PERSONAL_INFO_SUCCESS" | "LOAD_PERSONAL_INFO_FAIL";
export function loadPersonalInfo(): IAxiosActionCreator<LoadPersonalInfoActionType> {
  return {
    types: ["LOAD_PERSONAL_INFO", "LOAD_PERSONAL_INFO_SUCCESS", "LOAD_PERSONAL_INFO_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/personal-info"
      }
    }
  };
}

export const CREATE_PERSONAL_INFO = "CREATE_PERSONAL_INFO";
export const CREATE_PERSONAL_INFO_SUCCESS = "CREATE_PERSONAL_INFO_SUCCESS";
export const CREATE_PERSONAL_INFO_FAIL = "CREATE_PERSONAL_INFO_FAIL";
export type CreatePersonalInfoActionType = "CREATE_PERSONAL_INFO" | "CREATE_PERSONAL_INFO_SUCCESS" | "CREATE_PERSONAL_INFO_FAIL";
export function createPersonalInfo(data: any): IAxiosActionCreator<CreatePersonalInfoActionType> {
  return {
    types: ["CREATE_PERSONAL_INFO", "CREATE_PERSONAL_INFO_SUCCESS", "CREATE_PERSONAL_INFO_FAIL"],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/personal-info",
        data
      }
    }
  };
}

export const UPDATE_PERSONAL_INFO = "UPDATE_PERSONAL_INFO";
export const UPDATE_PERSONAL_INFO_SUCCESS = "UPDATE_PERSONAL_INFO_SUCCESS";
export const UPDATE_PERSONAL_INFO_FAIL = "UPDATE_PERSONAL_INFO_FAIL";
export type UpdatePersonalInfoActionType = "UPDATE_PERSONAL_INFO" | "UPDATE_PERSONAL_INFO_SUCCESS" | "UPDATE_PERSONAL_INFO_FAIL";
export function updatePersonalInfo(data: any): IAxiosActionCreator<UpdatePersonalInfoActionType> {
  return {
    types: [UPDATE_PERSONAL_INFO, UPDATE_PERSONAL_INFO_SUCCESS, UPDATE_PERSONAL_INFO_FAIL],
    payload: {
      request: {
        method: "PUT",
        url: "/identity/api/personal-info",
        data
      }
    }
  };
}

export const LOAD_ADDRESS_BOOK = "LOAD_ADDRESS_BOOK";
export const LOAD_ADDRESS_BOOK_SUCCESS = "LOAD_ADDRESS_BOOK_SUCCESS";
export const LOAD_ADDRESS_BOOK_FAIL = "LOAD_ADDRESS_BOOK_FAIL";
export type LoadAddressBookActionType = "LOAD_ADDRESS_BOOK" | "LOAD_ADDRESS_BOOK_SUCCESS" | "LOAD_ADDRESS_BOOK_FAIL";
export function loadAddressBook(): IAxiosActionCreator<LoadAddressBookActionType> {
  return {
    types: ["LOAD_ADDRESS_BOOK", "LOAD_ADDRESS_BOOK_SUCCESS", "LOAD_ADDRESS_BOOK_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: `/identity/api/address-book`
      }
    }
  };
}

export const ADD_ADDRESS = "ADD_ADDRESS";
export const ADD_ADDRESS_SUCCESS = "ADD_ADDRESS_SUCCESS";
export const ADD_ADDRESS_FAIL = "ADD_ADDRESS_FAIL";
export type AddAddressActionType = "ADD_ADDRESS" | "ADD_ADDRESS_SUCCESS" | "ADD_ADDRESS_FAIL";
export function addAddress(data: any): IAxiosActionCreator<AddAddressActionType> {
  return {
    types: ["ADD_ADDRESS", "ADD_ADDRESS_SUCCESS", "ADD_ADDRESS_FAIL"],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/address-book",
        data
      }
    }
  };
}

export const UPDATE_ADDRESS = "UPDATE_ADDRESS";
export const UPDATE_ADDRESS_SUCCESS = "UPDATE_ADDRESS_SUCCESS";
export const UPDATE_ADDRESS_FAIL = "UPDATE_ADDRESS_FAIL";
export type UpdateAddressActionType = "UPDATE_ADDRESS" | "UPDATE_ADDRESS_SUCCESS" | "UPDATE_ADDRESS_FAIL";
export function updateAddress(addressId: string, data: any): IAxiosActionCreator<UpdateAddressActionType> {
  return {
    types: ["UPDATE_ADDRESS", "UPDATE_ADDRESS_SUCCESS", "UPDATE_ADDRESS_FAIL"],
    payload: {
      request: {
        method: "PUT",
        url: `/identity/api/address-book/${addressId}`,
        data
      }
    }
  };
}

export const REMOVE_ADDRESS = "REMOVE_ADDRESS";
export const REMOVE_ADDRESS_SUCCESS = "REMOVE_ADDRESS_SUCCESS";
export const REMOVE_ADDRESS_FAIL = "REMOVE_ADDRESS_FAIL";
export type RemoveAddressActionType = "REMOVE_ADDRESS" | "REMOVE_ADDRESS_SUCCESS" | "REMOVE_ADDRESS_FAIL";
export function removeAddress(addressId: string): IAxiosActionCreator<RemoveAddressActionType> {
  return {
    types: ["REMOVE_ADDRESS", "REMOVE_ADDRESS_SUCCESS", "REMOVE_ADDRESS_FAIL"],
    payload: {
      request: {
        method: "DELETE",
        url: `/identity/api/address-book/${addressId}`
      }
    }
  };
}

export const CONFRIM_EMAIL = "CONFRIM_EMAIL";
export const CONFRIM_EMAIL_SUCCESS = "CONFRIM_EMAIL_SUCCESS";
export const CONFRIM_EMAIL_FAIL = "CONFRIM_EMAIL_FAIL";
export type ConfirmEmailActionType = "CONFRIM_EMAIL" | "CONFRIM_EMAIL_SUCCESS" | "CONFRIM_EMAIL_FAIL";
export function confirmEmail(userId: string, code: string): IAxiosActionCreator<ConfirmEmailActionType> {
  return {
    types: ["CONFRIM_EMAIL", "CONFRIM_EMAIL_SUCCESS", "CONFRIM_EMAIL_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: `/identity/api/email/confirm?userId=${userId}&code=${code}`
      }
    }
  };
}

export const FORGOT_PASSWORD = "FORGOT_PASSWORD";
export const FORGOT_PASSWORD_SUCCESS = "FORGOT_PASSWORD_SUCCESS";
export const FORGOT_PASSWORD_FAIL = "FORGOT_PASSWORD_FAIL";
export type ForgotPasswordActionType = "FORGOT_PASSWORD" | "FORGOT_PASSWORD_SUCCESS" | "FORGOT_PASSWORD_FAIL";
export function forgotPassword(data: any): IAxiosActionCreator<ForgotPasswordActionType> {
  return {
    types: ["FORGOT_PASSWORD", "FORGOT_PASSWORD_SUCCESS", "FORGOT_PASSWORD_FAIL"],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/password/forgot",
        data
      }
    }
  };
}

export const RESET_PASSWORD = "RESET_PASSWORD";
export const RESET_PASSWORD_SUCCESS = "RESET_PASSWORD_SUCCESS";
export const RESET_PASSWORD_FAIL = "RESET_PASSWORD_FAIL";
export type ResetPasswordActionType = "RESET_PASSWORD" | "RESET_PASSWORD_SUCCESS" | "RESET_PASSWORD_FAIL";
export function resetPassword(data: any): IAxiosActionCreator<ResetPasswordActionType> {
  return {
    types: ["RESET_PASSWORD", "RESET_PASSWORD_SUCCESS", "RESET_PASSWORD_FAIL"],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/password/reset",
        data
      }
    }
  };
}

export const LOAD_GAMES = "LOAD_GAMES";
export const LOAD_GAMES_SUCCESS = "LOAD_GAMES_SUCCESS";
export const LOAD_GAMES_FAIL = "LOAD_GAMES_FAIL";
export type LoadGamesActionType = "LOAD_GAMES" | "LOAD_GAMES_SUCCESS" | "LOAD_GAMES_FAIL";
export function loadGames(): IAxiosActionCreator<LoadGamesActionType> {
  return {
    types: ["LOAD_GAMES", "LOAD_GAMES_SUCCESS", "LOAD_GAMES_FAIL"],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/games"
      }
    }
  };
}
