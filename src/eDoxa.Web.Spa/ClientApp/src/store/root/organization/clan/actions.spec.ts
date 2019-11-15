import { loadClans, loadClan, createClan, leaveClan } from "./actions";
import {
  LOAD_CLANS,
  LOAD_CLANS_SUCCESS,
  LOAD_CLANS_FAIL,
  LOAD_CLAN,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  CREATE_CLAN,
  CREATE_CLAN_SUCCESS,
  CREATE_CLAN_FAIL,
  LEAVE_CLAN,
  LEAVE_CLAN_FAIL,
  LEAVE_CLAN_SUCCESS
} from "./types";

describe("clans", () => {
  it("should create an action to get all clans", () => {
    const expectedType = [LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/clans/api/clans";

    const actionCreator = loadClans();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific clan", () => {
    const clanId = "0";

    const expectedType = [LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/clans/${clanId}`;

    const actionCreator = loadClan(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to create a clan", () => {
    const data = { name: "clanName" };

    const expectedType = [CREATE_CLAN, CREATE_CLAN_SUCCESS, CREATE_CLAN_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/clans/api/clans";

    const actionCreator = createClan(data);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(data);
  });

  it("should create an action to leave the clan", () => {
    const clanId = "0";

    const expectedType = [LEAVE_CLAN, LEAVE_CLAN_SUCCESS, LEAVE_CLAN_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/clans/api/clans/${clanId}/members`;

    const actionCreator = leaveClan(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
