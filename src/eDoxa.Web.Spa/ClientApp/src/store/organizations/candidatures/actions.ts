import {
  LOAD_CANDIDATURES,
  LOAD_CANDIDATURES_SUCCESS,
  LOAD_CANDIDATURES_FAIL,
  LOAD_CANDIDATURE,
  LOAD_CANDIDATURE_SUCCESS,
  LOAD_CANDIDATURE_FAIL,
  ADD_CANDIDATURE,
  ADD_CANDIDATURE_SUCCESS,
  ADD_CANDIDATURE_FAIL,
  ACCEPT_CANDIDATURE,
  ACCEPT_CANDIDATURE_SUCCESS,
  ACCEPT_CANDIDATURE_FAIL,
  DECLINE_CANDIDATURE,
  DECLINE_CANDIDATURE_SUCCESS,
  DECLINE_CANDIDATURE_FAIL,
  CandidaturesActionCreators
} from "./types";

export function loadCandidaturesWithClanId(clanId: string): CandidaturesActionCreators {
  return {
    types: [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/candidatures?clanId=${clanId}`
      }
    }
  };
}

export function loadCandidaturesWithUserId(userId: string): CandidaturesActionCreators {
  return {
    types: [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/candidatures?clanId=${userId}`
      }
    }
  };
}

export function loadCandidature(candidatureId: string): CandidaturesActionCreators {
  return {
    types: [LOAD_CANDIDATURE, LOAD_CANDIDATURE_SUCCESS, LOAD_CANDIDATURE_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function addCandidature(data: any): CandidaturesActionCreators {
  return {
    types: [ADD_CANDIDATURE, ADD_CANDIDATURE_SUCCESS, ADD_CANDIDATURE_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/organizations/clans/api/candidatures",
        data
      }
    }
  };
}

export function acceptCandidature(candidatureId: string): CandidaturesActionCreators {
  return {
    types: [ACCEPT_CANDIDATURE, ACCEPT_CANDIDATURE_SUCCESS, ACCEPT_CANDIDATURE_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}

export function declineCandidature(candidatureId: string): CandidaturesActionCreators {
  return {
    types: [DECLINE_CANDIDATURE, DECLINE_CANDIDATURE_SUCCESS, DECLINE_CANDIDATURE_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/organizations/clans/api/candidatures/${candidatureId}`
      }
    }
  };
}
