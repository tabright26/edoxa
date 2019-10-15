import { reducer, initialState } from "./reducer";
import { AxiosError } from "axios";

const invitations204Data = [];
const invitations200Data = [{ clanId: "0", userId: "1" }, { clanId: "10", userId: "11" }, { clanId: "100", userId: "111" }];

const invitation200Data = { clanId: "0", userId: "1" };

describe("invitations reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle LOAD_INVITATIONS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_INVITATIONS_SUCCESS",
      payload: { status: 204, data: invitations204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_INVITATIONS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_INVITATIONS_SUCCESS",
      payload: { status: 200, data: invitations200Data }
    };
    const state = {
      data: invitations200Data,
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_INVITATIONS_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: "LOAD_INVITATIONS_FAIL",
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle LOAD_INVITATION_SUCCESS", () => {
    const action: any = {
      type: "LOAD_INVITATION_SUCCESS",
      payload: { data: invitation200Data }
    };
    const state = {
      data: [...initialState.data, invitation200Data],
      error: null,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  it("should handle LOAD_INVITATION_FAIL", () => {
    const error: AxiosError = {
      isAxiosError: true,
      config: {},
      name: "",
      message: ""
    };
    const action: any = {
      type: "LOAD_INVITATION_FAIL",
      error
    };
    const state = {
      data: initialState.data,
      error,
      loading: false
    };
    expect(reducer(initialState, action)).toEqual(state);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle ADD_INVITATION_SUCCESS", () => {
    const action: any = {
      type: "ADD_INVITATION_SUCCESS"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle ACCEPT_INVITATION_SUCCESS", () => {
    const action: any = {
      type: "ACCEPT_INVITATION_SUCCESS"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle DECLINE_INVITATION_SUCCESS", () => {
    const action: any = {
      type: "DECLINE_INVITATION_SUCCESS"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
