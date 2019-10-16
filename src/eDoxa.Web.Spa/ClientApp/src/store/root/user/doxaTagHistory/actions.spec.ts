import { LOAD_USER_DOXATAGHISTORY, LOAD_USER_DOXATAGHISTORY_SUCCESS, LOAD_USER_DOXATAGHISTORY_FAIL, UPDATE_USER_DOXATAG, UPDATE_USER_DOXATAG_SUCCESS, UPDATE_USER_DOXATAG_FAIL } from "./types";
import { loadDoxatagHistory, changeDoxaTag } from "./actions";

describe("identity actions", () => {
  it("should create an action to get user doxatag history", () => {
    const expectedType = [LOAD_USER_DOXATAGHISTORY, LOAD_USER_DOXATAGHISTORY_SUCCESS, LOAD_USER_DOXATAGHISTORY_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/doxatag-history";

    const actionCreator = loadDoxatagHistory();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user doxatag", () => {
    const expectedType = [UPDATE_USER_DOXATAG, UPDATE_USER_DOXATAG_SUCCESS, UPDATE_USER_DOXATAG_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/doxatag-history";
    const expectedDoxaTag = "DoxaTag";

    const actionCreator = changeDoxaTag(expectedDoxaTag);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(expectedDoxaTag);
  });
});
