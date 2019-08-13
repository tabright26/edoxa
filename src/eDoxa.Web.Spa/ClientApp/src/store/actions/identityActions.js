export const LOAD_USERS = "LOAD_USERS";
export const LOAD_USERS_SUCCESS = "LOAD_USERS_SUCCESS";
export const LOAD_USERS_FAIL = "LOAD_USERS_FAIL";
export function loadUsers() {
  return {
    types: [LOAD_USERS, LOAD_USERS_SUCCESS, LOAD_USERS_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/users"
      }
    }
  };
}

export const LOAD_USER_DOXATAG = "LOAD_USER_DOXATAG";
export const LOAD_USER_DOXATAG_SUCCESS = "LOAD_USER_DOXATAG_SUCCESS";
export const LOAD_USER_DOXATAG_FAIL = "LOAD_USER_DOXATAG_FAIL";
export function loadUserDoxatag(userId) {
  return {
    types: [LOAD_USER_DOXATAG, LOAD_USER_DOXATAG_SUCCESS, LOAD_USER_DOXATAG_FAIL],
    payload: {
      request: {
        method: "get",
        url: `/identity/api/users/${userId}/doxa-tag`
      }
    }
  };
}

export const LOAD_DOXATAG = "LOAD_DOXATAG";
export const LOAD_DOXATAG_SUCCESS = "LOAD_DOXATAG_SUCCESS";
export const LOAD_DOXATAG_FAIL = "LOAD_DOXATAG_FAIL";
export function loadDoxatag() {
  return {
    types: [LOAD_DOXATAG, LOAD_DOXATAG_SUCCESS, LOAD_DOXATAG_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/doxa-tag"
      }
    }
  };
}

export const UPDATE_DOXATAG = "UPDATE_DOXATAG";
export const UPDATE_DOXATAG_SUCCESS = "UPDATE_DOXATAG_SUCCESS";
export const UPDATE_DOXATAG_FAIL = "UPDATE_DOXATAG_FAIL";
export function updateDoxatag(body) {
  return {
    types: [UPDATE_DOXATAG, UPDATE_DOXATAG_SUCCESS, UPDATE_DOXATAG_FAIL],
    payload: {
      request: {
        method: "put",
        url: "/identity/api/doxa-tag",
        body: body
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

export const LOAD_ADDRESS = "LOAD_ADDRESS";
export const LOAD_ADDRESS_SUCCESS = "LOAD_ADDRESS_SUCCESS";
export const LOAD_ADDRESS_FAIL = "LOAD_ADDRESS_FAIL";
export function loadAddress() {
  return {
    types: [LOAD_ADDRESS, LOAD_ADDRESS_SUCCESS, LOAD_ADDRESS_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/identity/api/address"
      }
    }
  };
}

export const UPDATE_ADDRESS = "UPDATE_ADDRESS";
export const UPDATE_ADDRESS_SUCCESS = "UPDATE_ADDRESS_SUCCESS";
export const UPDATE_ADDRESS_FAIL = "UPDATE_ADDRESS_FAIL";
export function updateAddress(body) {
  return {
    types: [UPDATE_ADDRESS, UPDATE_ADDRESS_SUCCESS, UPDATE_ADDRESS_FAIL],
    payload: {
      request: {
        method: "put",
        url: "/identity/api/address",
        body: body
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
