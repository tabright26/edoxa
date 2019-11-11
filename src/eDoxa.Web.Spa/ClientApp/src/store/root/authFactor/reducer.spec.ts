import { reducer, initialState } from "./reducer";
import { AxiosError } from "axios";
import { LOAD_GAMES_SUCCESS, LOAD_GAMES_FAIL } from "./types";

const games200Data = [{ gameId: "League" }, { gameId: "Overwatch" }, { gameId: "CSGO" }];

describe("user games reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_GAMES_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_GAMES_SUCCESS,
      payload: { status: 204, data: initialState.data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_GAMES_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_GAMES_SUCCESS,
      payload: { status: 200, data: games200Data }
    };
    const state = {
      data: games200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_GAMES_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_GAMES_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
});
