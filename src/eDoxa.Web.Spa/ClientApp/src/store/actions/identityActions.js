export const LOAD_DOXATAGS = "LOAD_DOXATAGS";
export const LOAD_DOXATAGS_SUCCESS = "LOAD_DOXATAGS_SUCCESS";
export const LOAD_DOXATAGS_FAIL = "LOAD_DOXATAGS_FAIL";
export function loadDoxaTags() {
  return {
    types: [LOAD_DOXATAGS, LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/doxatags"
      }
    }
  };
}

export const LOAD_DOXATAG_HISTORY = "LOAD_DOXATAG_HISTORY";
export const LOAD_DOXATAG_HISTORY_SUCCESS = "LOAD_DOXATAG_HISTORY_SUCCESS";
export const LOAD_DOXATAG_HISTORY_FAIL = "LOAD_DOXATAG_HISTORY_FAIL";
export function loadDoxaTagHistory() {
  return {
    types: [LOAD_DOXATAG_HISTORY, LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/doxatag-history"
      }
    }
  };
}

export const CHANGE_DOXATAG = "CHANGE_DOXATAG";
export const CHANGE_DOXATAG_SUCCESS = "CHANGE_DOXATAG_SUCCESS";
export const CHANGE_DOXATAG_FAIL = "CHANGE_DOXATAG_FAIL";
export function changeDoxaTag(data) {
  return {
    types: [CHANGE_DOXATAG, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL],
    payload: {
      request: {
        method: "post",
        url: "/identity/api/doxatag-history",
        data
      }
    }
  };
}

export const LOAD_PERSONAL_INFO = "LOAD_PERSONAL_INFO";
export const LOAD_PERSONAL_INFO_SUCCESS = "LOAD_PERSONAL_INFO_SUCCESS";
export const LOAD_PERSONAL_INFO_FAIL = "LOAD_PERSONAL_INFO_FAIL";
export function loadPersonalInfo() {
  return {
    types: [LOAD_PERSONAL_INFO, LOAD_PERSONAL_INFO_SUCCESS, LOAD_PERSONAL_INFO_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/personal-info"
      }
    }
  };
}

export const CREATE_PERSONAL_INFO = "CREATE_PERSONAL_INFO";
export const CREATE_PERSONAL_INFO_SUCCESS = "CREATE_PERSONAL_INFO_SUCCESS";
export const CREATE_PERSONAL_INFO_FAIL = "CREATE_PERSONAL_INFO_FAIL";
export function createPersonalInfo(data) {
  return {
    types: [CREATE_PERSONAL_INFO, CREATE_PERSONAL_INFO_SUCCESS, CREATE_PERSONAL_INFO_FAIL],
    payload: {
      request: {
        method: "post",
        url: "/identity/api/personal-info",
        data
      }
    }
  };
}

export const UPDATE_PERSONAL_INFO = "UPDATE_PERSONAL_INFO";
export const UPDATE_PERSONAL_INFO_SUCCESS = "UPDATE_PERSONAL_INFO_SUCCESS";
export const UPDATE_PERSONAL_INFO_FAIL = "UPDATE_PERSONAL_INFO_FAIL";
export function updatePersonalInfo(data) {
  return {
    types: [UPDATE_PERSONAL_INFO, UPDATE_PERSONAL_INFO_SUCCESS, UPDATE_PERSONAL_INFO_FAIL],
    payload: {
      request: {
        method: "put",
        url: "/identity/api/personal-info",
        data
      }
    }
  };
}

export const LOAD_ADDRESS_BOOK = "LOAD_ADDRESS_BOOK";
export const LOAD_ADDRESS_BOOK_SUCCESS = "LOAD_ADDRESS_BOOK_SUCCESS";
export const LOAD_ADDRESS_BOOK_FAIL = "LOAD_ADDRESS_BOOK_FAIL";
export function loadAddressBook() {
  return {
    types: [LOAD_ADDRESS_BOOK, LOAD_ADDRESS_BOOK_SUCCESS, LOAD_ADDRESS_BOOK_FAIL],
    payload: {
      request: {
        method: "get",
        url: `/identity/api/address-book`
      }
    }
  };
}

export const ADD_ADDRESS = "ADD_ADDRESS";
export const ADD_ADDRESS_SUCCESS = "ADD_ADDRESS_SUCCESS";
export const ADD_ADDRESS_FAIL = "ADD_ADDRESS_FAIL";
export function addAddress(data) {
  return {
    types: [ADD_ADDRESS, ADD_ADDRESS_SUCCESS, ADD_ADDRESS_FAIL],
    payload: {
      request: {
        method: "post",
        url: "/identity/api/address-book",
        data
      }
    }
  };
}

export const UPDATE_ADDRESS = "UPDATE_ADDRESS";
export const UPDATE_ADDRESS_SUCCESS = "UPDATE_ADDRESS_SUCCESS";
export const UPDATE_ADDRESS_FAIL = "UPDATE_ADDRESS_FAIL";
export function updateAddress(addressId, data) {
  return {
    types: [UPDATE_ADDRESS, UPDATE_ADDRESS_SUCCESS, UPDATE_ADDRESS_FAIL],
    payload: {
      request: {
        method: "put",
        url: `/identity/api/address-book/${addressId}`,
        data
      }
    }
  };
}

export const REMOVE_ADDRESS = "REMOVE_ADDRESS";
export const REMOVE_ADDRESS_SUCCESS = "REMOVE_ADDRESS_SUCCESS";
export const REMOVE_ADDRESS_FAIL = "REMOVE_ADDRESS_FAIL";
export function removeAddress(addressId) {
  return {
    types: [REMOVE_ADDRESS, REMOVE_ADDRESS_SUCCESS, REMOVE_ADDRESS_FAIL],
    payload: {
      request: {
        method: "delete",
        url: `/identity/api/address-book/${addressId}`
      }
    }
  };
}

export const CONFRIM_EMAIL = "CONFRIM_EMAIL";
export const CONFRIM_EMAIL_SUCCESS = "CONFRIM_EMAIL_SUCCESS";
export const CONFRIM_EMAIL_FAIL = "CONFRIM_EMAIL_FAIL";
export function confirmEmail(userId, code) {
  return {
    types: [CONFRIM_EMAIL, CONFRIM_EMAIL_SUCCESS, CONFRIM_EMAIL_FAIL],
    payload: {
      request: {
        method: "get",
        url: `/identity/api/email/confirm?userId=${userId}&code=${code}`
      }
    }
  };
}

export const FORGOT_PASSWORD = "FORGOT_PASSWORD";
export const FORGOT_PASSWORD_SUCCESS = "FORGOT_PASSWORD_SUCCESS";
export const FORGOT_PASSWORD_FAIL = "FORGOT_PASSWORD_FAIL";
export function forgotPassword(data) {
  return {
    types: [FORGOT_PASSWORD, FORGOT_PASSWORD_SUCCESS, FORGOT_PASSWORD_FAIL],
    payload: {
      request: {
        method: "post",
        url: "/identity/api/password/forgot",
        data
      }
    }
  };
}

export const RESET_PASSWORD = "RESET_PASSWORD";
export const RESET_PASSWORD_SUCCESS = "RESET_PASSWORD_SUCCESS";
export const RESET_PASSWORD_FAIL = "RESET_PASSWORD_FAIL";
export function resetPassword(data) {
  return {
    types: [RESET_PASSWORD, RESET_PASSWORD_SUCCESS, RESET_PASSWORD_FAIL],
    payload: {
      request: {
        method: "post",
        url: "/identity/api/password/reset",
        data
      }
    }
  };
}

export const LOAD_GAMES = "LOAD_GAMES";
export const LOAD_GAMES_SUCCESS = "LOAD_GAMES_SUCCESS";
export const LOAD_GAMES_FAIL = "LOAD_GAMES_FAIL";
export function loadGames() {
  return {
    types: [LOAD_GAMES, LOAD_GAMES_SUCCESS, LOAD_GAMES_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/games"
      }
    }
  };
}
