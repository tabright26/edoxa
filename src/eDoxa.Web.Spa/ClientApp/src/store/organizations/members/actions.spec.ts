import { loadMembers, kickMember, leaveClan } from "./actions";
import { LOAD_MEMBERS, LOAD_MEMBERS_SUCCESS, LOAD_MEMBERS_FAIL, KICK_MEMBER, KICK_MEMBER_SUCCESS, KICK_MEMBER_FAIL, LEAVE_CLAN, LEAVE_CLAN_SUCCESS, LEAVE_CLAN_FAIL } from "./types";

describe("members", () => {
  it("should create an action to get a specific clan members", () => {
    const clanId = "0";

    const expectedType = [LOAD_MEMBERS, LOAD_MEMBERS_SUCCESS, LOAD_MEMBERS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}/members`;

    const actionCreator = loadMembers(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to kick a member", () => {
    const clanId = "0";
    const memberId = "10";

    const expectedType = [KICK_MEMBER, KICK_MEMBER_SUCCESS, KICK_MEMBER_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}/members/${memberId}`;

    const actionCreator = kickMember(clanId, memberId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to leave the clan", () => {
    const clanId = "0";

    const expectedType = [LEAVE_CLAN, LEAVE_CLAN_SUCCESS, LEAVE_CLAN_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}/members`;

    const actionCreator = leaveClan(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
