import { LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL } from "./types";
import { loadCandidaturesWithClanId, loadCandidaturesWithUserId } from "./actions";

describe("candidatures actions", () => {
  it("should create an action to get all candidatures", () => {
    const clanId = "404201a0-fc83-4d1a-a3ce-d7a99561f7d9";
    const expectedType = [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/candidatures?clanId=${clanId}`;

    const actionCreator = loadCandidaturesWithClanId(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get all candidatures", () => {
    const userId = "404201a0-fc83-4d1a-a3ce-d7a99561f7d9";
    const expectedType = [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/candidatures?userId=${userId}`;

    const actionCreator = loadCandidaturesWithUserId(userId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
