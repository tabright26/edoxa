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
  ClansActionCreators
} from "./types";

export function loadClans(): ClansActionCreators {
  return {
    types: [LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/organizations/clans/api/clans"
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
        url: `/organizations/clans/api/clans/${clanId}`
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
