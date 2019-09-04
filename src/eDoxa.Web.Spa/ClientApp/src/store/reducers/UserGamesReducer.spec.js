import { reducer, initialState } from "./userGamesReducer";
import * as types from "../actions/identityActions";

const games204Data = [];
const games200Data = [{ gameId: "League" }, { gameId: "Overwatch" }, { gameId: "CSGO" }];

describe("user games reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_GAMES_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_GAMES_SUCCESS,
        payload: { status: 204, data: games204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_GAMES_SUCCESS 200", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_GAMES_SUCCESS,
        payload: { status: 200, data: games200Data }
      })
    ).toEqual(games200Data);
  });

  it("should handle LOAD_GAMES_FAIL", () => {
    expect(
      reducer(initialState, {
        type: types.LOAD_GAMES_FAIL
      })
    ).toEqual(initialState);
  });
});
