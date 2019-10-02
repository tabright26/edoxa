import { reducer, initialState } from "./reducer";

const invitations204Data = [];
const invitations200Data = [{ invitation: "Invitation1" }, { invitation: "Invitation2" }, { invitation: "Invitation3" }];

describe("invitations reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

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
    expect(reducer(initialState, action)).toEqual(invitations200Data);
  });

  it("should handle LOAD_INVITATIONS_FAIL", () => {
    const action: any = {
      type: "LOAD_INVITATIONS_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
