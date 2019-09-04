import { reducer } from "./arenaGamesLeagueOfLegendsReducer";
import * as types from "../actions/leagueOfLegendsActions";

describe("arena games league of legends reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(undefined, {})).toEqual({});
  });

  it("should handle LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS", () => {
    expect(
      reducer(
        {},
        {
          type: types.LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS,
          payload: { data: "Run the tests" }
        }
      )
    ).toEqual("Run the tests");
  });

  it("should handle LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL", () => {
    expect(
      reducer(
        {},
        {
          type: types.LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL,
          payload: { data: "This is an error message" }
        }
      )
    ).toEqual({});
  });
});
