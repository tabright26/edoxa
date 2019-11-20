import { DOWNLOAD_CLAN_LOGO, DOWNLOAD_CLAN_LOGO_SUCCESS, DOWNLOAD_CLAN_LOGO_FAIL, UPLOAD_CLAN_LOGO, UPLOAD_CLAN_LOGO_SUCCESS, UPLOAD_CLAN_LOGO_FAIL, ClanLogosActionCreators } from "./types";

export function downloadClanLogo(clanId: string): ClanLogosActionCreators {
  return {
    types: [DOWNLOAD_CLAN_LOGO, DOWNLOAD_CLAN_LOGO_SUCCESS, DOWNLOAD_CLAN_LOGO_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/clans/${clanId}/logo`
      }
    }
  };
}

export function uploadClanLogo(clanId: string, data: any): ClanLogosActionCreators {
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
