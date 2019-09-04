import { reducer } from "./userDoxaTagHistoryReducer";
import * as types from "../actions/identityActions";

describe("user doxatag history reducer", () => {
  it("should return the initial state", () => {
    expect(reducer([], {})).toEqual([]);
  });

  it("should handle LOAD_DOXATAG_HISTORY_SUCCESS 204", () => {
    expect(
      reducer([], {
        type: types.LOAD_DOXATAG_HISTORY_SUCCESS,
        payload: { status: 204, data: "Run the tests" }
      })
    ).toEqual([]);
  });

  it("should handle LOAD_DOXATAG_HISTORY_SUCCESS Empty", () => {
    expect(
      reducer([], {
        type: types.LOAD_DOXATAG_HISTORY_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual("Run the tests");
  });

  it("should handle LOAD_DOXATAG_HISTORY_FAIL", () => {
    expect(
      reducer([], {
        type: types.LOAD_DOXATAG_HISTORY_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual([]);
  });
});
