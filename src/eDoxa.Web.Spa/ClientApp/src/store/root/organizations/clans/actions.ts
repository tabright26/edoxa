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
  DOWNLOAD_CLAN_LOGO,
  DOWNLOAD_CLAN_LOGO_SUCCESS,
  DOWNLOAD_CLAN_LOGO_FAIL,
  UPLOAD_CLAN_LOGO,
  UPLOAD_CLAN_LOGO_SUCCESS,
  UPLOAD_CLAN_LOGO_FAIL,
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

export function downloadClanLogo(clanId: string): ClansActionCreators {
  return {
    types: [DOWNLOAD_CLAN_LOGO, DOWNLOAD_CLAN_LOGO_SUCCESS, DOWNLOAD_CLAN_LOGO_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/clans/${clanId}/logo`
      }
    }
  };
}

export function uploadClanLogo(clanId: string, data: any): ClansActionCreators {
  return {
    types: [UPLOAD_CLAN_LOGO, UPLOAD_CLAN_LOGO_SUCCESS, UPLOAD_CLAN_LOGO_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/organizations/clans/api/clans/${clanId}/logo`,
        data
      }
    }
  };
}
