import { reducer, initialState } from "./reducer";
import * as types from "../../../actions/identityActions";

const doxaTagHistory204Data = [];
const doxaTagHistory200Data = [{ doxaTag: "DoxaTag1" }, { doxaTag: "DoxaTag2" }, { doxaTag: "DoxaTag3" }];

describe("user doxatag history reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAG_HISTORY_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_DOXATAG_HISTORY_SUCCESS,
        payload: { status: 204, data: doxaTagHistory204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAG_HISTORY_SUCCESS 200", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_DOXATAG_HISTORY_SUCCESS,
        payload: { status: 200, data: doxaTagHistory200Data }
      })
    ).toEqual(doxaTagHistory200Data);
  });

  it("should handle LOAD_DOXATAG_HISTORY_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_DOXATAG_HISTORY_FAIL
      })
    ).toEqual(initialState);
  });
});
