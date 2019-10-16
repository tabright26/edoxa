import { reducer, initialState } from "./reducer";
import {
  LOAD_USER_DOXATAGHISTORY,
  LOAD_USER_DOXATAGHISTORY_SUCCESS,
  LOAD_USER_DOXATAGHISTORY_FAIL,
  UPDATE_USER_DOXATAG,
  UPDATE_USER_DOXATAG_SUCCESS,
  UPDATE_USER_DOXATAG_FAIL,
  UserDoxatagHistoryActionCreators
} from "./types";
import { AxiosError } from "axios";

const doxaTagHistory204Data = [];
const doxaTagHistory200Data = [{ doxaTag: "DoxaTag1" }, { doxaTag: "DoxaTag2" }, { doxaTag: "DoxaTag3" }];

describe("user doxatag history reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAG_HISTORY_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_USER_DOXATAGHISTORY_SUCCESS,
      payload: { status: 204, data: doxaTagHistory204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_DOXATAG_HISTORY_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_USER_DOXATAGHISTORY_SUCCESS,
      payload: { status: 200, data: doxaTagHistory200Data }
    };
    const state = {
      data: doxaTagHistory200Data,
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
