import { LOAD_DOXATAG_HISTORY, LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL, CHANGE_DOXATAG, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL } from "./types";
import { loadDoxatagHistory, changeDoxaTag } from "./actions";

describe("identity actions", () => {
  it("should create an action to get user doxatag history", () => {
    const expectedType = [LOAD_DOXATAG_HISTORY, LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/doxatag-history";

    const actionCreator = loadDoxatagHistory();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user doxatag", () => {
    const expectedType = [CHANGE_DOXATAG, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL];
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
