import { LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL } from "./types";
import { loadCandidaturesWithClanId, loadCandidaturesWithUserId } from "./actions";

describe("candidatures actions", () => {
  it("should create an action to get all candidatures", () => {
    const expectedType = [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/organizations/api/clans";

    const actionCreator = loadCandidaturesWithClanId();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get all candidatures", () => {
    const expectedType = [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/organizations/api/clans";

    const actionCreator = loadCandidaturesWithUserId();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
