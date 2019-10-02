import { LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL, InvitationsActionCreators } from "./types";

export function loadInvitations(): InvitationsActionCreators {
  return {
    types: [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/organizations/api/clans"
      }
    }
  };
}
