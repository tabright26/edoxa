import { reducer, initialState } from "./reducer";

const doxaTag204Data = [];
const doxaTag200Data = "DoxaTagYo";

describe("doxatag reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAGS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_DOXATAGS_SUCCESS",
      payload: { status: 204, data: doxaTag204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAGS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_DOXATAGS_SUCCESS",
      payload: { status: 200, data: doxaTag200Data }
    };
    expect(reducer(initialState, action)).toEqual(doxaTag200Data);
  });

  it("should handle LOAD_DOXATAGS_FAIL", () => {
    const action: any = {
      type: "LOAD_DOXATAGS_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
