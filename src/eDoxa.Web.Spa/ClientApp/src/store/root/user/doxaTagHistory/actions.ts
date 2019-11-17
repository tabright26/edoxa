import {
  LOAD_USER_DOXATAGHISTORY,
  LOAD_USER_DOXATAGHISTORY_SUCCESS,
  LOAD_USER_DOXATAGHISTORY_FAIL,
  UPDATE_USER_DOXATAG,
  UPDATE_USER_DOXATAG_SUCCESS,
  UPDATE_USER_DOXATAG_FAIL,
  UserDoxatagHistoryActionCreators
} from "./types";

export function loadUserDoxatagHistory(): UserDoxatagHistoryActionCreators {
  return {
    types: [LOAD_USER_DOXATAGHISTORY, LOAD_USER_DOXATAGHISTORY_SUCCESS, LOAD_USER_DOXATAGHISTORY_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/doxatag-history"
      }
    }
  };
}

export function updateUserDoxatag(data: any): UserDoxatagHistoryActionCreators {
  return {
    types: [UPDATE_USER_DOXATAG, UPDATE_USER_DOXATAG_SUCCESS, UPDATE_USER_DOXATAG_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/doxatag-history",
        data
      }
    }
  };
}
