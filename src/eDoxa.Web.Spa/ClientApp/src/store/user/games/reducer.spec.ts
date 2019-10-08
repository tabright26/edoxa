import { reducer, initialState } from "./reducer";

const games204Data = [];
const games200Data = [{ gameId: "League" }, { gameId: "Overwatch" }, { gameId: "CSGO" }];

describe("user games reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_GAMES_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_GAMES_SUCCESS",
      payload: { status: 204, data: games204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_GAMES_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_GAMES_SUCCESS",
      payload: { status: 200, data: games200Data }
    };
    expect(reducer(initialState, action)).toEqual(games200Data);
  });

  it("should handle LOAD_GAMES_FAIL", () => {
    const action: any = {
      type: "LOAD_GAMES_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});