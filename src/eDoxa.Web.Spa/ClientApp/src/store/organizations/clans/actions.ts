import {
  LOAD_CLANS,
  LOAD_CLANS_SUCCESS,
  LOAD_CLANS_FAIL,
  LOAD_CLAN,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  ADD_CLAN,
  ADD_CLAN_SUCCESS,
  ADD_CLAN_FAIL,
  LOAD_LOGO,
  LOAD_LOGO_SUCCESS,
  LOAD_LOGO_FAIL,
  UPDATE_LOGO,
  UPDATE_LOGO_SUCCESS,
  UPDATE_LOGO_FAIL,
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

export function addClan(data: any): ClansActionCreators {
  return {
    types: [ADD_CLAN, ADD_CLAN_SUCCESS, ADD_CLAN_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/organizations/clans/api/clans",
        data
      }
    }
  };
}

export function loadLogo(clanId: string): ClansActionCreators {
  return {
    types: [LOAD_LOGO, LOAD_LOGO_SUCCESS, LOAD_LOGO_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/clans/${clanId}/logo`
      }
    }
  };
}

export function updateLogo(clanId: string, data: any): ClansActionCreators {
  return {
    types: [UPDATE_LOGO, UPDATE_LOGO_SUCCESS, UPDATE_LOGO_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/organizations/clans/api/clans/${clanId}/logo`,
        data
      }
    }
  };
}
