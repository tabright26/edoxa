import { reducer, initialState } from "./reducer";
import {
  LOAD_CLAN_MEMBERS_SUCCESS,
  LOAD_CLAN_MEMBERS_FAIL,
  KICK_CLAN_MEMBER_SUCCESS
} from "store/actions/clan/types";
import { AxiosError } from "axios";

const members204Data = [];
const members200Data = [
  { userId: "0", clanId: "10" },
  { clanId: "1", userId: "10" },
  { clanId: "3", userId: "10" }
];

describe("invitations reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_MEMBERS_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_CLAN_MEMBERS_SUCCESS,
      payload: { status: 204, data: members204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_MEMBERS_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_CLAN_MEMBERS_SUCCESS,
      payload: { status: 200, data: members200Data }
    };
    const state = {
      data: members200Data,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_MEMBERS_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: LOAD_CLAN_MEMBERS_FAIL,
      error
    };
    const state = {
      data: initialState.data,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle KICK_MEMBER_SUCCESS", () => {
    const action: any = {
      type: KICK_CLAN_MEMBER_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
