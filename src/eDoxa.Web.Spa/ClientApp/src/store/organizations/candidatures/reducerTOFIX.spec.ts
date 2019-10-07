import { reducer, initialState } from "./reducer";

const candidatures204Data = [];
const candidatures200Data = [{ candidature: "Candidature1" }, { candidature: "Candidature2" }, { candidature: "Candidature3" }];

describe("candidatures reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CANDIDATURES_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_CANDIDATURES_SUCCESS",
      payload: { status: 204, data: candidatures204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CANDIDATURES_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_CANDIDATURES_SUCCESS",
      payload: { status: 200, data: candidatures200Data }
    };
    expect(reducer(initialState, action)).toEqual(candidatures200Data);
  });

  it("should handle LOAD_CANDIDATURES_FAIL", () => {
    const action: any = {
      type: "LOAD_CANDIDATURES_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
