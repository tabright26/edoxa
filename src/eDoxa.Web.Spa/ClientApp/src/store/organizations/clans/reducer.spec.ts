import { reducer, initialState } from "./reducer";

const clans204Data = [];
const clans200Data = [
  { id: "0", name: "Clan1", summary: "This is a summary", ownerId: "0", members: [{ userId: "0", clanId: "0" }] },
  { id: "1", name: "Clan2", summary: "This is a summary", ownerId: "1", members: [{ userId: "1", clanId: "1" }] },
  { id: "2", name: "Clan3", summary: "This is a summary", ownerId: "2", members: [{ userId: "2", clanId: "2" }] }
];

const clan200Data = { id: "0", name: "Clan1", summary: "This is a summary", ownerId: "0", members: [{ userId: "0", clanId: "0" }] };

//TODO
const logo200Data = { data: "How to mock an image  ?" };

describe("candidatures reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle LOAD_CLANS_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_CLANS_SUCCESS",
      payload: { status: 204, data: clans204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CLANS_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_CLANS_SUCCESS",
      payload: { status: 200, data: clans200Data }
    };
    expect(reducer(initialState, action)).toEqual(clans200Data);
  });

  it("should handle LOAD_CLANS_FAIL", () => {
    const action: any = {
      type: "LOAD_CLANS_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle LOAD_CLAN_SUCCESS", () => {
    const action: any = {
      type: "LOAD_CLAN_SUCCESS",
      payload: { data: clan200Data }
    };
    expect(reducer(initialState, action)).toEqual([...initialState, clan200Data]);
  });

  it("should handle LOAD_CLAN_FAIL", () => {
    const action: any = {
      type: "LOAD_CLAN_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle ADD_CLAN_SUCCESS", () => {
    const action: any = {
      type: "ADD_CLAN_SUCCESS"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------

  it("should handle LOAD_LOGO_SUCCESS", () => {
    const action: any = {
      type: "LOAD_LOGO_SUCCESS",
      payload: { data: logo200Data }
    };
    expect(reducer(initialState, action)).toEqual([...initialState, logo200Data]);
  });

  it("should handle LOAD_LOGO_FAIL", () => {
    const action: any = {
      type: "LOAD_LOGO_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
  //--------------------------------------------------------------------------------------------------------

  it("should handle UPDATE_LOGO_SUCCESS", () => {
    const action: any = {
      type: "UPDATE_LOGO_SUCCESS"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  //--------------------------------------------------------------------------------------------------------
});
