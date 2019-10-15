import { reducer, initialState } from "./reducer";

const personalInfo204Data = {};
const personalInfo200Data = { name: "Gabriel", gender: "Male" };

describe("user personal info reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_PERSONAL_INFO_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_PERSONAL_INFO_SUCCESS",
      payload: { status: 204, data: personalInfo204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_PERSONAL_INFO_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_PERSONAL_INFO_SUCCESS",
      payload: { status: 200, data: personalInfo200Data }
    };
    const state = {
      data: personalInfo200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_PERSONAL_INFO_FAIL", () => {
    const action: any = {
      type: "LOAD_PERSONAL_INFO_FAIL"
    };
    const state = {
      data: initialState.data,
      error: "LOAD_PERSONAL_INFO_FAIL",
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
});
