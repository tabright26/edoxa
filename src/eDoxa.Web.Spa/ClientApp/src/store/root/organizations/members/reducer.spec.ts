import { reducer, initialState } from "./reducer";
import { LOAD_MEMBERS_SUCCESS, LOAD_MEMBERS_FAIL, LEAVE_CLAN_SUCCESS, KICK_MEMBER_SUCCESS } from "./types";

const members204Data = [];
const members200Data = [{ userId: "0", clanId: "10" }, { clanId: "1", userId: "10" }, { clanId: "3", userId: "10" }];

describe("invitations reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle LOAD_MEMBERS_SUCCESS 204", () => {
    const action: any = {
      type: LOAD_MEMBERS_SUCCESS,
      payload: { status: 204, data: members204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_MEMBERS_SUCCESS 200", () => {
    const action: any = {
      type: LOAD_MEMBERS_SUCCESS,
      payload: { status: 200, data: members200Data }
    };
    const state = {
      data: members200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_MEMBERS_FAIL", () => {
    const action: any = {
      type: LOAD_MEMBERS_FAIL
    };
    const state = {
      data: initialState.data,
      error: LOAD_MEMBERS_FAIL,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle LEAVE_CLAN_SUCCESS", () => {
    const action: any = {
      type: LEAVE_CLAN_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  // it("should handle LEAVE_CLAN_FAIL", () => {
  //   const action: any = {
  //     type: LEAVE_CLAN_FAIL,
  //     error: {
  //       isAxiosError: true,
  //       response: {
  //         data: []
  //       }
  //     }
  //   };
  //   expect(reducer(initialState, action)).toThrow();
  // });

  //--------------------------------------------------------------------------------------------------------

  it("should handle KICK_MEMBER_SUCCESS", () => {
    const action: any = {
      type: KICK_MEMBER_SUCCESS
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------
});
