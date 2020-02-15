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
  LoadClanCandidaturesActionCreator,
  LoadClanCandidatureActionCreator,
  SendClanCandidatureActionCreator,
  AcceptClanCandidatureActionCreator,
  DeclineClanCandidatureActionCreator,
  LoadClansActionCreator,
  LoadClanActionCreator,
  CreateClanActionCreator,
  LeaveClanActionCreator,
  LoadClanInvitationsActionCreator,
  LoadClanInvitationActionCreator,
  SendClanInvitationActionCreator,
  AcceptClanInvitationActionCreator,
  DeclineClanInvitationActionCreator,
  DownloadClanLogoActionCreator,
  UploadClanLogoActionCreator,
  LoadClanMembersActionCreator,
  KickClanMemberActionCreator
} from "./types";
import { AXIOS_PAYLOAD_CLIENT_DEFAULT } from "utils/axios/types";
import { InvitationId, ClanId, ClanMemberId } from "types/clans";
import { UserId } from "types/identity";

export function loadClanCandidatures(
  type: string,
  id: string
): LoadClanCandidaturesActionCreator {
  return {
    types: [
      LOAD_CLAN_CANDIDATURES,
      LOAD_CLAN_CANDIDATURES_SUCCESS,
      LOAD_CLAN_CANDIDATURES_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `/clans/api/candidatures?${type}Id=${id}` // FRANCIS: This is wrong.
      }
    }
  };
}

export function loadClanCandidature(
  candidatureId: string
): LoadClanCandidatureActionCreator {
  return {
    types: [
      LOAD_CLAN_CANDIDATURE,
      LOAD_CLAN_CANDIDATURE_SUCCESS,
      LOAD_CLAN_CANDIDATURE_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
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
): SendClanCandidatureActionCreator {
  return {
    types: [
      SEND_CLAN_CANDIDATURE,
      SEND_CLAN_CANDIDATURE_SUCCESS,
      SEND_CLAN_CANDIDATURE_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
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
): AcceptClanCandidatureActionCreator {
  return {
    types: [
      ACCEPT_CLAN_CANDIDATURE,
      ACCEPT_CLAN_CANDIDATURE_SUCCESS,
      ACCEPT_CLAN_CANDIDATURE_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function declineClanCandidature(
  candidatureId: string
): DeclineClanCandidatureActionCreator {
  return {
    types: [
      DECLINE_CLAN_CANDIDATURE,
      DECLINE_CLAN_CANDIDATURE_SUCCESS,
      DECLINE_CLAN_CANDIDATURE_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "DELETE",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function loadClans(): LoadClansActionCreator {
  return {
    types: [LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: "/clans/api/clans"
      }
    }
  };
}

export function loadClan(clanId: string): LoadClanActionCreator {
  return {
    types: [LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `/clans/api/clans/${clanId}`
      }
    }
  };
}

export function createClan(data: any): CreateClanActionCreator {
  return {
    types: [CREATE_CLAN, CREATE_CLAN_SUCCESS, CREATE_CLAN_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: "/organizations/clans/api/clans",
        data
      }
    }
  };
}

export function leaveClan(clanId: string): LeaveClanActionCreator {
  return {
    types: [LEAVE_CLAN, LEAVE_CLAN_SUCCESS, LEAVE_CLAN_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
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
): LoadClanInvitationsActionCreator {
  return {
    types: [
      LOAD_CLAN_INVITATIONS,
      LOAD_CLAN_INVITATIONS_SUCCESS,
      LOAD_CLAN_INVITATIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `/clans/api/invitations?${type}Id=${id}`
      }
    }
  };
}

export function loadClanInvitation(
  invitationId: InvitationId
): LoadClanInvitationActionCreator {
  return {
    types: [
      LOAD_CLAN_INVITATION,
      LOAD_CLAN_INVITATION_SUCCESS,
      LOAD_CLAN_INVITATION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function sendClanInvitation(
  clanId: ClanId,
  userId: UserId
): SendClanInvitationActionCreator {
  return {
    types: [
      SEND_CLAN_INVITATION,
      SEND_CLAN_INVITATION_SUCCESS,
      SEND_CLAN_INVITATION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
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
  invitationId: InvitationId
): AcceptClanInvitationActionCreator {
  return {
    types: [
      ACCEPT_CLAN_INVITATION,
      ACCEPT_CLAN_INVITATION_SUCCESS,
      ACCEPT_CLAN_INVITATION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: `/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function declineClanInvitation(
  invitationId: InvitationId
): DeclineClanInvitationActionCreator {
  return {
    types: [
      DECLINE_CLAN_INVITATION,
      DECLINE_CLAN_INVITATION_SUCCESS,
      DECLINE_CLAN_INVITATION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "DELETE",
        url: `/clans/api/invitations/${invitationId}`
      }
    }
  };
}

export function downloadClanLogo(
  clanId: ClanId
): DownloadClanLogoActionCreator {
  return {
    types: [
      DOWNLOAD_CLAN_LOGO,
      DOWNLOAD_CLAN_LOGO_SUCCESS,
      DOWNLOAD_CLAN_LOGO_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `/clans/api/clans/${clanId}/logo`
      }
    }
  };
}

export function uploadClanLogo(
  clanId: ClanId,
  data: any
): UploadClanLogoActionCreator {
  return {
    types: [UPLOAD_CLAN_LOGO, UPLOAD_CLAN_LOGO_SUCCESS, UPLOAD_CLAN_LOGO_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: `/clans/api/clans/${clanId}/logo`,
        data
      }
    }
  };
}

export function loadClanMembers(clanId: ClanId): LoadClanMembersActionCreator {
  return {
    types: [
      LOAD_CLAN_MEMBERS,
      LOAD_CLAN_MEMBERS_SUCCESS,
      LOAD_CLAN_MEMBERS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `/clans/api/clans/${clanId}/members`
      }
    }
  };
}

export function kickClanMember(
  clanId: ClanId,
  memberId: ClanMemberId
): KickClanMemberActionCreator {
  return {
    types: [KICK_CLAN_MEMBER, KICK_CLAN_MEMBER_SUCCESS, KICK_CLAN_MEMBER_FAIL],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "DELETE",
        url: `/clans/api/clans/${clanId}/members/${memberId}`
      }
    }
  };
}
