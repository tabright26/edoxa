import { LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL, ClanActionCreators } from "./types";

export function loadClan(): ClanActionCreators {
  return {
    types: [LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/organizations/api/clans/clanId"
      }
    }
  };
}
