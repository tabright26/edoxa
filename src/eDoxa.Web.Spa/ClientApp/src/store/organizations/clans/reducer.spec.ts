import { reducer, initialState } from "./reducer";

const clans204Data = [];
const clans200Data = [{ clan: "Clan1" }, { clan: "Clan2" }, { clan: "Clan3" }];

describe("clans reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CLANS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_CLANS_SUCCESS",
      payload: { status: 204, data: clans204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CLANS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_CLANS_SUCCESS",
      payload: { status: 200, data: clans200Data }
    };
    expect(reducer(initialState, action)).toEqual(clans200Data);
  });

  it("should handle LOAD_CLANS_FAIL", () => {
    const action: any = {
      type: "LOAD_CLANS_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
