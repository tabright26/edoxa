import { reducer, initialState } from "./reducer";
import * as types from "../../../actions/identityActions";

const personalInfo204Data = {};
const personalInfo200Data = { name: "Gabriel", gender: "Male" };

describe("user personal info reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_PERSONAL_INFO_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_PERSONAL_INFO_SUCCESS,
        payload: { status: 204, data: personalInfo204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_PERSONAL_INFO_SUCCESS 200", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_PERSONAL_INFO_SUCCESS,
        payload: { status: 200, data: personalInfo200Data }
      })
    ).toEqual(personalInfo200Data);
  });

  it("should handle LOAD_PERSONAL_INFO_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_PERSONAL_INFO_FAIL
      })
    ).toEqual(initialState);
  });
});
