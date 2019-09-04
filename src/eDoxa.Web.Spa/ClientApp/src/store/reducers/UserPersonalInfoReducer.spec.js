import { reducer } from "./userPersonalInfoReducer";
import * as types from "../actions/identityActions";

describe("user personal info reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(null, {})).toEqual(null);
  });

  it("should handle LOAD_PERSONAL_INFO_SUCCESS 204", () => {
    expect(
      reducer(null, {
        type: types.LOAD_PERSONAL_INFO_SUCCESS,
        payload: { status: 204, data: "Run the tests" }
      })
    ).toEqual(null);
  });

  it("should handle LOAD_PERSONAL_INFO_SUCCESS Empty", () => {
    expect(
      reducer(null, {
        type: types.LOAD_PERSONAL_INFO_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual("Run the tests");
  });

  it("should handle LOAD_PERSONAL_INFO_FAIL", () => {
    expect(
      reducer(null, {
        type: types.LOAD_PERSONAL_INFO_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual(null);
  });
});
