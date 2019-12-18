import { reducer, initialState } from "./reducer";
import { AxiosError } from "axios";
import {
  LOAD_USER_DOXATAGHISTORY_SUCCESS,
  LOAD_USER_DOXATAGHISTORY_FAIL
} from "store/actions/identity/types";

const doxatagHistory204Data = [];
const doxatagHistory200Data = [
  { doxatag: "Doxatag1" },
  { doxatag: "Doxatag2" },
  { doxatag: "Doxatag3" }
];

describe("user doxatag history reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAG_HISTORY_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_USER_DOXATAGHISTORY_SUCCESS,
      payload: { status: 204, data: doxatagHistory204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAG_HISTORY_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_USER_DOXATAGHISTORY_SUCCESS,
      payload: { status: 200, data: doxatagHistory200Data }
    };
    const state = {
      data: doxatagHistory200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_DOXATAG_HISTORY_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_USER_DOXATAGHISTORY_FAIL,
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
