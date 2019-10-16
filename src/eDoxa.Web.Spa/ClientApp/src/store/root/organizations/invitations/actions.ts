import {
  LOAD_CLAN_INVITATIONS,
  LOAD_CLAN_INVITATIONS_SUCCESS,
  LOAD_CLAN_INVITATIONS_FAIL,
  LOAD_CLAN_INVITATION,
  LOAD_CLAN_INVITATION_SUCCESS,
  LOAD_CLAN_INVITATION_FAIL,
  SEND_CLAN_INVITATION,
  SEND_CLAN_INVITATION_SUCCESS,
  SEND_CLAN_INVITATION_FAIL,
  ACCEPT_CLAN_INVITATION,
  ACCEPT_CLAN_INVITATION_SUCCESS,
  ACCEPT_CLAN_INVITATION_FAIL,
  DECLINE_CLAN_INVITATION,
  DECLINE_CLAN_INVITATION_SUCCESS,
  DECLINE_CLAN_INVITATION_FAIL,
  ClanInvitationsActionCreators
} from "./types";

export function loadInvitations(type: string, id: string): ClanInvitationsActionCreators {
  return {
    types: [LOAD_CLAN_INVITATIONS, LOAD_CLAN_INVITATIONS_SUCCESS, LOAD_CLAN_INVITATIONS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/invitations?${type}Id=${id}`
      }
    }
  };
}

export function loadInvitation(invitationId: string): ClanInvitationsActionCreators {
  return {
    types: [LOAD_CLAN_INVITATION, LOAD_CLAN_INVITATION_SUCCESS, LOAD_CLAN_INVITATION_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function addInvitation(clanId, userId): ClanInvitationsActionCreators {
  return {
    types: [SEND_CLAN_INVITATION, SEND_CLAN_INVITATION_SUCCESS, SEND_CLAN_INVITATION_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/organizations/clans/api/invitations",
        data: {
          clanId,
          userId
        }
      }
    }
  };
}

export function acceptInvitation(invitationId: string): ClanInvitationsActionCreators {
  return {
    types: [ACCEPT_CLAN_INVITATION, ACCEPT_CLAN_INVITATION_SUCCESS, ACCEPT_CLAN_INVITATION_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/organizations/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function declineInvitation(invitationId: string): ClanInvitationsActionCreators {
  return {
    types: [DECLINE_CLAN_INVITATION, DECLINE_CLAN_INVITATION_SUCCESS, DECLINE_CLAN_INVITATION_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/organizations/clans/api/invitations/${invitationId}`
      }
    }
  };
}
