import {
  LOAD_CLAN_MEMBERS,
  LOAD_CLAN_MEMBERS_SUCCESS,
  LOAD_CLAN_MEMBERS_FAIL,
  KICK_CLAN_MEMBER,
  KICK_CLAN_MEMBER_SUCCESS,
  KICK_CLAN_MEMBER_FAIL,

  ClanMembersActionCreators
} from "./types";

export function loadClanMembers(clanId: string): ClanMembersActionCreators {
  return {
    types: [LOAD_CLAN_MEMBERS, LOAD_CLAN_MEMBERS_SUCCESS, LOAD_CLAN_MEMBERS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/clans/api/clans/${clanId}/members`
      }
    }
  };
}

export function kickClanMember(clanId: string, memberId: string): ClanMembersActionCreators {
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


