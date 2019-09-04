import { reducer } from "./userGamesReducer";
import * as types from "../actions/identityActions";

describe("user account transactions reducer", () => {
  it("should return the initial state", () => {
    expect(reducer([], {})).toEqual([]);
  });

  it("should handle LOAD_GAMES_SUCCESS 204", () => {
    expect(
      reducer([], {
        type: types.LOAD_GAMES_SUCCESS,
        payload: { status: 204, data: "Run the tests" }
      })
    ).toEqual([]);
  });

  it("should handle LOAD_GAMES_SUCCESS Empty", () => {
    expect(
      reducer([], {
        type: types.LOAD_GAMES_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual("Run the tests");
  });

  it("should handle LOAD_GAMES_FAIL", () => {
    expect(
      reducer([], {
        type: types.LOAD_GAMES_FAIL,
        payload: { data: "This is an error message" }
      })
    ).toEqual([]);
  });
});
