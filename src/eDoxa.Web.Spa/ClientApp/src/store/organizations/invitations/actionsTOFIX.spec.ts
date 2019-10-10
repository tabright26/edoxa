import { LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL } from "./types";
import { loadInvitationsWithClanId, loadInvitationsWithUserId } from "./actions";

describe("invitations actions", () => {
  it("should create an action to get all invitations", () => {
    const clanId = "";
    const expectedType = [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/invitations?clanId=${clanId}`;

    const actionCreator = loadInvitationsWithClanId(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get all invitations", () => {
    const userId = "";
    const expectedType = [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/invitations?userId=${userId}`;

    const actionCreator = loadInvitationsWithUserId(userId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
