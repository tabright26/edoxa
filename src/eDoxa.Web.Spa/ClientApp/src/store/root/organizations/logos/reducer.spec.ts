import { reducer, initialState } from "./reducer";
import { DOWNLOAD_CLAN_LOGO_SUCCESS, DOWNLOAD_CLAN_LOGO_FAIL, UPLOAD_CLAN_LOGO_SUCCESS } from "./types";
import { AxiosError } from "axios";

//TODO
const logo200Data = { data: "How to mock an image  ?" };

describe("candidatures reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_LOGO_SUCCESS", () => {
    const action: any = {
      type: DOWNLOAD_CLAN_LOGO_SUCCESS,
      payload: { data: logo200Data }
    };
    const state = {
      data: [...initialState.data, logo200Data],
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
  it("should handle LOAD_LOGO_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: DOWNLOAD_CLAN_LOGO_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
  it("should handle UPDATE_LOGO_SUCCESS", () => {
    const action: any = {
      type: UPLOAD_CLAN_LOGO_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
