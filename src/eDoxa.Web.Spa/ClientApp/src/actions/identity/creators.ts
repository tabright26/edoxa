import { IAxiosActionCreator } from "interfaces/axios";

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

export type UpdatePersonalInfoActionType = "UPDATE_PERSONAL_INFO" | "UPDATE_PERSONAL_INFO_SUCCESS" | "UPDATE_PERSONAL_INFO_FAIL";
export function updatePersonalInfo(data: any): IAxiosActionCreator<UpdatePersonalInfoActionType> {
  return {
    types: ["UPDATE_PERSONAL_INFO", "UPDATE_PERSONAL_INFO_SUCCESS", "UPDATE_PERSONAL_INFO_FAIL"],
    payload: {
      request: {
        method: "PUT",
        url: "/identity/api/personal-info",
        data
      }
    }
  };
}

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
