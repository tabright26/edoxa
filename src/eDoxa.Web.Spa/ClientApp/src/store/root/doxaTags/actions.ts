import { LOAD_DOXATAGS, LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL, DoxatagsActionCreators } from "./types";

export function loadDoxatags(): DoxatagsActionCreators {
  return {
    types: [LOAD_DOXATAGS, LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/doxatags"
      }
    }
  };
}
