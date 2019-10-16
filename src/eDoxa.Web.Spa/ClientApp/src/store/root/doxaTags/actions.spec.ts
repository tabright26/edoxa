import { loadDoxatags } from "./actions";
import { LOAD_DOXATAGS, LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL } from "./types";

describe("identity actions", () => {
  it("should create an action to get user doxatag", () => {
    const expectedType = [LOAD_DOXATAGS, LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/doxatags";

    const actionCreator = loadDoxatags();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
