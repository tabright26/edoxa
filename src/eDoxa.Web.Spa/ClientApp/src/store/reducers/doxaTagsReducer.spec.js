import { reducer, initialState } from "./doxaTagsReducer";
import * as types from "../actions/identityActions";

const doxaTag204Data = [];
const doxaTag200Data = "DoxaTagYo";

describe("doxatag reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAGS_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_DOXATAGS_SUCCESS,
        payload: { status: 204, data: doxaTag204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAGS_SUCCESS 200", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_DOXATAGS_SUCCESS,
        payload: { status: 200, data: doxaTag200Data }
      })
    ).toEqual(doxaTag200Data);
  });

  it("should handle LOAD_DOXATAGS_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_DOXATAGS_FAIL
      })
    ).toEqual(initialState);
  });
});
