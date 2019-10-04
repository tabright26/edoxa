import { reducer, initialState } from "./reducer";

const members204Data = [];
const members200Data = [{ member: "Member1" }, { member: "Member1" }, { member: "Member3" }];

describe("invitations reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_MEMBERS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_MEMBERS_SUCCESS",
      payload: { status: 204, data: members204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_MEMBERS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_MEMBERS_SUCCESS",
      payload: { status: 200, data: members200Data }
    };
    expect(reducer(initialState, action)).toEqual(members200Data);
  });

  it("should handle LOAD_MEMBERS_FAIL", () => {
    const action: any = {
      type: "LOAD_MEMBERS_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
