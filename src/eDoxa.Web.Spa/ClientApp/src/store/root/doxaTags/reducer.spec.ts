import { reducer, initialState } from "./reducer";
import { LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL } from "./types";
import { AxiosError } from "axios";

const doxatag204Data = [];
const doxatag200Data = "DoxatagYo";

describe("doxatag reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAGS_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_DOXATAGS_SUCCESS,
      payload: { status: 204, data: doxatag204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAGS_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_DOXATAGS_SUCCESS,
      payload: { status: 200, data: doxatag200Data }
    };
    const state = {
      data: doxatag200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_DOXATAGS_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_DOXATAGS_FAIL,
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
