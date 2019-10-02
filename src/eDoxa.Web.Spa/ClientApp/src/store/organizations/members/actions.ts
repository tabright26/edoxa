import { LOAD_MEMBERS, LOAD_MEMBERS_SUCCESS, LOAD_MEMBERS_FAIL, MembersActionCreators } from "./types";

export function loadMembers(): MembersActionCreators {
  return {
    types: [LOAD_MEMBERS, LOAD_MEMBERS_SUCCESS, LOAD_MEMBERS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/organizations/api/clans"
      }
    }
  };
}
