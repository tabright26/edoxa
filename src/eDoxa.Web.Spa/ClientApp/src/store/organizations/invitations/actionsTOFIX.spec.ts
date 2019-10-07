import { LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL } from "./types";
import { loadInvitationsWithClanId, loadInvitationsWithUserId } from "./actions";

describe("invitations actions", () => {
  it("should create an action to get all invitations", () => {
    const expectedType = [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/organizations/api/clans";

    const actionCreator = loadInvitationsWithClanId();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get all invitations", () => {
    const expectedType = [LOAD_INVITATIONS, LOAD_INVITATIONS_SUCCESS, LOAD_INVITATIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/organizations/api/clans";

    const actionCreator = loadInvitationsWithUserId();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
