import { reducer, initialState } from "./reducer";
import { LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL } from "./types";

const successData = { id: "1" };

describe("arena games league of legends reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS", () => {
    expect(reducer(initialState, { type: LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS, payload: { data: successData } })).toEqual(successData);
  });

  it("should handle LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL", () => {
    expect(reducer(initialState, { type: LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL })).toEqual(initialState);
  });
});
