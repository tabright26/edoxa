import { LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL, ClansActionCreators } from "./types";

export function loadClans(): ClansActionCreators {
  return {
    types: [LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/organizations/api/clans"
      }
    }
  };
}
