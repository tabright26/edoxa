import {
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
  REFUSE_CLAN_CANDIDATURE,
  REFUSE_CLAN_CANDIDATURE_SUCCESS,
  REFUSE_CLAN_CANDIDATURE_FAIL,
  ClanCandidaturesActionCreators
} from "./types";

export function loadClanCandidatures(type: string, id: string): ClanCandidaturesActionCreators {
  return {
    types: [LOAD_CLAN_CANDIDATURES, LOAD_CLAN_CANDIDATURES_SUCCESS, LOAD_CLAN_CANDIDATURES_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/candidatures?${type}Id=${id}` // FRANCIS: This is wrong.
      }
    }
  };
}

export function loadClanCandidature(candidatureId: string): ClanCandidaturesActionCreators {
  return {
    types: [LOAD_CLAN_CANDIDATURE, LOAD_CLAN_CANDIDATURE_SUCCESS, LOAD_CLAN_CANDIDATURE_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function sendClanCandidature(clanId: string, userId: string): ClanCandidaturesActionCreators {
  return {
    types: [SEND_CLAN_CANDIDATURE, SEND_CLAN_CANDIDATURE_SUCCESS, SEND_CLAN_CANDIDATURE_FAIL],
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

export function acceptClanCandidature(candidatureId: string): ClanCandidaturesActionCreators {
  return {
    types: [ACCEPT_CLAN_CANDIDATURE, ACCEPT_CLAN_CANDIDATURE_SUCCESS, ACCEPT_CLAN_CANDIDATURE_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function declineClanCandidature(candidatureId: string): ClanCandidaturesActionCreators {
  return {
    types: [REFUSE_CLAN_CANDIDATURE, REFUSE_CLAN_CANDIDATURE_SUCCESS, REFUSE_CLAN_CANDIDATURE_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}
