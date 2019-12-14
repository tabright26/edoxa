import {
  LOAD_CLANS,
  LOAD_CLANS_SUCCESS,
  LOAD_CLANS_FAIL,
  LOAD_CLAN,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  CREATE_CLAN,
  CREATE_CLAN_SUCCESS,
  CREATE_CLAN_FAIL,
  LEAVE_CLAN,
  LEAVE_CLAN_SUCCESS,
  LEAVE_CLAN_FAIL,
  LOAD_CLAN_CANDIDATURES,
  LOAD_CLAN_CANDIDATURES_SUCCESS,
  LOAD_CLAN_CANDIDATURES_FAIL,
  LOAD_CLAN_CANDIDATURE,
  LOAD_CLAN_CANDIDATURE_SUCCESS,
  LOAD_CLAN_CANDIDATURE_FAIL,
  SEND_CLAN_CANDIDATURE,
  SEND_CLAN_CANDIDATURE_SUCCESS,
  SEND_CLAN_CANDIDATURE_FAIL,
  ACCEPT_CLAN_CANDIDATURE,
  ACCEPT_CLAN_CANDIDATURE_SUCCESS,
  ACCEPT_CLAN_CANDIDATURE_FAIL,
  DECLINE_CLAN_CANDIDATURE,
  DECLINE_CLAN_CANDIDATURE_SUCCESS,
  DECLINE_CLAN_CANDIDATURE_FAIL,
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
  LOAD_CLAN_MEMBERS,
  LOAD_CLAN_MEMBERS_SUCCESS,
  LOAD_CLAN_MEMBERS_FAIL,
  KICK_CLAN_MEMBER,
  KICK_CLAN_MEMBER_SUCCESS,
  KICK_CLAN_MEMBER_FAIL,
  DOWNLOAD_CLAN_LOGO,
  DOWNLOAD_CLAN_LOGO_SUCCESS,
  DOWNLOAD_CLAN_LOGO_FAIL,
  UPLOAD_CLAN_LOGO,
  UPLOAD_CLAN_LOGO_SUCCESS,
  UPLOAD_CLAN_LOGO_FAIL,
  ClansActionCreators,
  ClanCandidaturesActionCreators,
  ClanInvitationsActionCreators,
  ClanMembersActionCreators,
  ClanLogosActionCreators
} from "./types";

export function loadClanCandidatures(
  type: string,
  id: string
): ClanCandidaturesActionCreators {
  return {
    types: [
      LOAD_CLAN_CANDIDATURES,
      LOAD_CLAN_CANDIDATURES_SUCCESS,
      LOAD_CLAN_CANDIDATURES_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/candidatures?${type}Id=${id}` // FRANCIS: This is wrong.
      }
    }
  };
}

export function loadClanCandidature(
  candidatureId: string
): ClanCandidaturesActionCreators {
  return {
    types: [
      LOAD_CLAN_CANDIDATURE,
      LOAD_CLAN_CANDIDATURE_SUCCESS,
      LOAD_CLAN_CANDIDATURE_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function sendClanCandidature(
  clanId: string,
  userId: string
): ClanCandidaturesActionCreators {
  return {
    types: [
      SEND_CLAN_CANDIDATURE,
      SEND_CLAN_CANDIDATURE_SUCCESS,
      SEND_CLAN_CANDIDATURE_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/organizations/clans/api/candidatures",
        data: {
          clanId,
          userId
        }
      }
    }
  };
}

export function acceptClanCandidature(
  candidatureId: string
): ClanCandidaturesActionCreators {
  return {
    types: [
      ACCEPT_CLAN_CANDIDATURE,
      ACCEPT_CLAN_CANDIDATURE_SUCCESS,
      ACCEPT_CLAN_CANDIDATURE_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function declineClanCandidature(
  candidatureId: string
): ClanCandidaturesActionCreators {
  return {
    types: [
      DECLINE_CLAN_CANDIDATURE,
      DECLINE_CLAN_CANDIDATURE_SUCCESS,
      DECLINE_CLAN_CANDIDATURE_FAIL
    ],
    payload: {
      request: {
        method: "DELETE",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function loadClans(): ClansActionCreators {
  return {
    types: [LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/clans/api/clans"
      }
    }
  };
}

export function loadClan(clanId: string): ClansActionCreators {
  return {
    types: [LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/clans/${clanId}`
      }
    }
  };
}

export function createClan(data: any): ClansActionCreators {
  return {
    types: [CREATE_CLAN, CREATE_CLAN_SUCCESS, CREATE_CLAN_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/organizations/clans/api/clans",
        data
      }
    }
  };
}

export function leaveClan(clanId: string): ClansActionCreators {
  return {
    types: [LEAVE_CLAN, LEAVE_CLAN_SUCCESS, LEAVE_CLAN_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/organizations/clans/api/clans/${clanId}/members`
      }
    }
  };
}

export function loadClanInvitations(
  type: string,
  id: string
): ClanInvitationsActionCreators {
  return {
    types: [
      LOAD_CLAN_INVITATIONS,
      LOAD_CLAN_INVITATIONS_SUCCESS,
      LOAD_CLAN_INVITATIONS_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/invitations?${type}Id=${id}`
      }
    }
  };
}

export function loadClanInvitation(
  invitationId: string
): ClanInvitationsActionCreators {
  return {
    types: [
      LOAD_CLAN_INVITATION,
      LOAD_CLAN_INVITATION_SUCCESS,
      LOAD_CLAN_INVITATION_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function sendClanInvitation(
  clanId,
  userId
): ClanInvitationsActionCreators {
  return {
    types: [
      SEND_CLAN_INVITATION,
      SEND_CLAN_INVITATION_SUCCESS,
      SEND_CLAN_INVITATION_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/clans/api/invitations",
        data: {
          clanId,
          userId
        }
      }
    }
  };
}

export function acceptClanInvitation(
  invitationId: string
): ClanInvitationsActionCreators {
  return {
    types: [
      ACCEPT_CLAN_INVITATION,
      ACCEPT_CLAN_INVITATION_SUCCESS,
      ACCEPT_CLAN_INVITATION_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: `/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function declineClanInvitation(
  invitationId: string
): ClanInvitationsActionCreators {
  return {
    types: [
      DECLINE_CLAN_INVITATION,
      DECLINE_CLAN_INVITATION_SUCCESS,
      DECLINE_CLAN_INVITATION_FAIL
    ],
    payload: {
      request: {
        method: "DELETE",
        url: `/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function downloadClanLogo(clanId: string): ClanLogosActionCreators {
  return {
    types: [
      DOWNLOAD_CLAN_LOGO,
      DOWNLOAD_CLAN_LOGO_SUCCESS,
      DOWNLOAD_CLAN_LOGO_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/clans/${clanId}/logo`
      }
    }
  };
}

export function uploadClanLogo(
  clanId: string,
  data: any
): ClanLogosActionCreators {
  return {
    types: [UPLOAD_CLAN_LOGO, UPLOAD_CLAN_LOGO_SUCCESS, UPLOAD_CLAN_LOGO_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/clans/api/clans/${clanId}/logo`,
        data
      }
    }
  };
}

export function loadClanMembers(clanId: string): ClanMembersActionCreators {
  return {
    types: [
      LOAD_CLAN_MEMBERS,
      LOAD_CLAN_MEMBERS_SUCCESS,
      LOAD_CLAN_MEMBERS_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/clans/${clanId}/members`
      }
    }
  };
}

export function kickClanMember(
  clanId: string,
  memberId: string
): ClanMembersActionCreators {
  return {
    types: [KICK_CLAN_MEMBER, KICK_CLAN_MEMBER_SUCCESS, KICK_CLAN_MEMBER_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/clans/api/clans/${clanId}/members/${memberId}`
      }
    }
  };
}
