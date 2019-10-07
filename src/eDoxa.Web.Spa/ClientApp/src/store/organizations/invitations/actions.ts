import {
  LOAD_INVITATIONS,
  LOAD_INVITATIONS_SUCCESS,
  LOAD_INVITATIONS_FAIL,
  LOAD_INVITATION,
  LOAD_INVITATION_SUCCESS,
  LOAD_INVITATION_FAIL,
  ADD_INVITATION,
  ADD_INVITATION_SUCCESS,
  ADD_INVITATION_FAIL,
  ACCEPT_INVITATION,
  ACCEPT_INVITATION_SUCCESS,
  ACCEPT_INVITATION_FAIL,
  DECLINE_INVITATION,
  DECLINE_INVITATION_SUCCESS,
  DECLINE_INVITATION_FAIL,
  InvitationsActionCreators
} from "./types";

export function loadInvitationsWithClanId(clanId: string): InvitationsActionCreators {
  return {
    types: [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/invitations?clanId=${clanId}`
      }
    }
  };
}

export function loadInvitationsWithUserId(userId: string): InvitationsActionCreators {
  return {
    types: [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/invitations?clanId=${userId}`
      }
    }
  };
}

export function loadInvitation(invitationId: string): InvitationsActionCreators {
  return {
    types: [LOAD_INVITATION, LOAD_INVITATION_SUCCESS, LOAD_INVITATION_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function addInvitation(data: any): InvitationsActionCreators {
  return {
    types: [ADD_INVITATION, ADD_INVITATION_SUCCESS, ADD_INVITATION_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/organizations/clans/api/invitations",
        data
      }
    }
  };
}

export function acceptInvitation(invitationId: string): InvitationsActionCreators {
  return {
    types: [ACCEPT_INVITATION, ACCEPT_INVITATION_SUCCESS, ACCEPT_INVITATION_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/organizations/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function declineInvitation(invitationId: string): InvitationsActionCreators {
  return {
    types: [DECLINE_INVITATION, DECLINE_INVITATION_SUCCESS, DECLINE_INVITATION_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/organizations/clans/api/invitations/${invitationId}`
      }
    }
  };
}
