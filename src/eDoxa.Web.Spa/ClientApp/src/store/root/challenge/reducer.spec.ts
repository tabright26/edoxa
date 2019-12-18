import { reducer, initialState } from "./reducer";
import { AxiosError } from "axios";
import { LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL } from "store/actions/challenge/types";

const challenges204Data = { data: [], error: null, loading: false };
const challenges200Data = { data: [{ id: "1" }, { id: "2" }, { id: "1" }], error: null, loading: false };

const challengeSuccessData = { id: "1" };
const challengeExpectedState = [...initialState.data, challengeSuccessData];

describe("arena challenges reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CHALLENGES_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_CHALLENGES_SUCCESS,
      payload: { status: 204, data: challenges204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CHALLENGES_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_CHALLENGES_SUCCESS,
      payload: { status: 200, data: challenges200Data.data }
    };
    expect(reducer(initialState, action)).toEqual(challenges200Data);
  });

  it("should handle LOAD_CHALLENGES_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_CHALLENGES_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_CHALLENGE_SUCCESS", () => {
    const action: any = {
      type: LOAD_CHALLENGE_SUCCESS,
      payload: {
        data: challengeSuccessData
      }
    };
    const state = {
      data: challengeExpectedState,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_CHALLENGE_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_CHALLENGE_FAIL,
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
