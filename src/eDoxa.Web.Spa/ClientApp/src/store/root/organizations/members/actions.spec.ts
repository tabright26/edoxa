import { loadClanMembers, kickClanMember } from "./actions";
import {
  LOAD_CLAN_MEMBERS,
  LOAD_CLAN_MEMBERS_SUCCESS,
  LOAD_CLAN_MEMBERS_FAIL,
  KICK_CLAN_MEMBER,
  KICK_CLAN_MEMBER_SUCCESS,
  KICK_CLAN_MEMBER_FAIL
} from "./types";

describe("members", () => {
  it("should create an action to get a specific clan members", () => {
    const clanId = "0";

    const expectedType = [LOAD_CLAN_MEMBERS, LOAD_CLAN_MEMBERS_SUCCESS, LOAD_CLAN_MEMBERS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}/members`;

    const actionCreator = loadClanMembers(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to kick a member", () => {
    const clanId = "0";
    const memberId = "10";

    const expectedType = [KICK_CLAN_MEMBER, KICK_CLAN_MEMBER_SUCCESS, KICK_CLAN_MEMBER_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}/members/${memberId}`;

    const actionCreator = kickClanMember(clanId, memberId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
