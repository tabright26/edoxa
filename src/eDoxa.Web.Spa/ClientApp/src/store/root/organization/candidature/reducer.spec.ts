import { reducer, initialState } from "./reducer";
import {
  LOAD_CLAN_CANDIDATURES_SUCCESS,
  LOAD_CLAN_CANDIDATURES_FAIL,
  LOAD_CLAN_CANDIDATURE_SUCCESS,
  LOAD_CLAN_CANDIDATURE_FAIL,
  SEND_CLAN_CANDIDATURE_SUCCESS,
  ACCEPT_CLAN_CANDIDATURE_SUCCESS,
  DECLINE_CLAN_CANDIDATURE_SUCCESS
} from "store/actions/clan/types";
import { AxiosError } from "axios";

const candidatures204Data = [];
const candidatures200Data = [
  { clanId: "0", userId: "1" },
  { clanId: "10", userId: "11" },
  { clanId: "100", userId: "111" }
];

const candidature200Data = { clanId: "0", userId: "1" };

describe("candidatures reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });
  it("should handle LOAD_CANDIDATURES_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_CLAN_CANDIDATURES_SUCCESS,
      payload: { status: 204, data: candidatures204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
  it("should handle LOAD_CANDIDATURES_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_CLAN_CANDIDATURES_SUCCESS,
      payload: { status: 200, data: candidatures200Data }
    };
    const state = {
      data: candidatures200Data,

      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
  it("should handle LOAD_CANDIDATURES_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_CLAN_CANDIDATURES_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
  it("should handle LOAD_CANDIDATURE_SUCCESS", () => {
    const action: any = {
      type: LOAD_CLAN_CANDIDATURE_SUCCESS,
      payload: { data: candidature200Data }
    };
    const state = {
      data: [...initialState.data, candidature200Data],
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
  it("should handle LOAD_CANDIDATURE_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_CLAN_CANDIDATURE_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });
  it("should handle ADD_CANDIDATURE_SUCCESS", () => {
    const action: any = {
      type: SEND_CLAN_CANDIDATURE_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
  it("should handle ACCEPT_CANDIDATURE_SUCCESS", () => {
    const action: any = {
      type: ACCEPT_CLAN_CANDIDATURE_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
  it("should handle DECLINE_CANDIDATURE_SUCCESS", () => {
    const action: any = {
      type: DECLINE_CLAN_CANDIDATURE_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
