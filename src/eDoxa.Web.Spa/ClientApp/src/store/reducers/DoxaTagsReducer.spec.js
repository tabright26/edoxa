import { reducer } from "./doxaTagsReducer";
import * as types from "../actions/identityActions";

describe("doxatag reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(undefined, {})).toEqual([]);
  });

  it("should handle LOAD_DOXATAGS_SUCCESS 204", () => {
    expect(
      reducer([], {
        type: types.LOAD_DOXATAGS_SUCCESS,
        payload: { status: 204, data: "Run the tests" }
      })
    ).toEqual([]);
  });

  it("should handle LOAD_DOXATAGS_SUCCESS Empty", () => {
    expect(
      reducer([], {
        type: types.LOAD_DOXATAGS_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual("Run the tests");
  });

  it("should handle LOAD_DOXATAGS_FAIL", () => {
    expect(
      reducer([], {
        type: types.LOAD_DOXATAGS_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual([]);
  });
});
