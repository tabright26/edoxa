import { reducer, initialState } from "./reducer";

const clan204Data = [];
const clan200Data = [{ clan: "Clan" }];

describe("clan reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CLAN_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_CLAN_SUCCESS",
      payload: { status: 204, data: clan204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CLAN_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_CLAN_SUCCESS",
      payload: { status: 200, data: clan200Data }
    };
    expect(reducer(initialState, action)).toEqual(clan200Data);
  });

  it("should handle LOAD_CLAN_FAIL", () => {
    const action: any = {
      type: "LOAD_CLAN_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
