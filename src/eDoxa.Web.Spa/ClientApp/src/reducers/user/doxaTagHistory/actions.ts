import { LOAD_DOXATAG_HISTORY, LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL, CHANGE_DOXATAG, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL, DoxatagHistoryActionCreators } from "./types";

export function loadDoxaTagHistory(): DoxatagHistoryActionCreators {
  return {
    types: [LOAD_DOXATAG_HISTORY, LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/doxatag-history"
      }
    }
  };
}

export function changeDoxaTag(data: any): DoxatagHistoryActionCreators {
  return {
    types: [CHANGE_DOXATAG, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/doxatag-history",
        data
      }
    }
  };
}
