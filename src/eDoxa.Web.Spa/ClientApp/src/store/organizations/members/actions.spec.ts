import { LOAD_MEMBERS, LOAD_MEMBERS_SUCCESS, LOAD_MEMBERS_FAIL } from "./types";
import { loadMembers } from "./actions";

describe("invitations actions", () => {
  it("should create an action to get all invitations", () => {
    const expectedType = [LOAD_MEMBERS, LOAD_MEMBERS_SUCCESS, LOAD_MEMBERS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/organizations/api/clans";

    const actionCreator = loadMembers();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
