import { LOAD_LOGO, LOAD_LOGO_SUCCESS, LOAD_LOGO_FAIL, UPDATE_LOGO, UPDATE_LOGO_SUCCESS, UPDATE_LOGO_FAIL, LogoActionCreators } from "./types";

export function loadLogo(clanId: string): LogoActionCreators {
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

export function updateLogo(clanId: string, data: any): LogoActionCreators {
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
