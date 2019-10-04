import {
  LOAD_MEMBERS,
  LOAD_MEMBERS_SUCCESS,
  LOAD_MEMBERS_FAIL,
  KICK_MEMBER,
  KICK_MEMBER_SUCCESS,
  KICK_MEMBER_FAIL,
  LEAVE_CLAN,
  LEAVE_CLAN_SUCCESS,
  LEAVE_CLAN_FAIL,
  MembersActionCreators
} from "./types";

export function loadMembers(clanId: string): MembersActionCreators {
  return {
    types: [LOAD_MEMBERS, LOAD_MEMBERS_SUCCESS, LOAD_MEMBERS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/organizations/clans/api/clans/${clanId}/members`
      }
    }
  };
}

export function kickMember(clanId: string, memberId: string): MembersActionCreators {
  return {
    types: [KICK_MEMBER, KICK_MEMBER_SUCCESS, KICK_MEMBER_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/organizations/clans/api/clans/${clanId}/members/${memberId}`
      }
    }
  };
}

export function leaveClan(clanId: string): MembersActionCreators {
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
