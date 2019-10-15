import { loadClans, loadClan, createClan, downloadClanLogo, uploadClanLogo } from "./actions";
import {
  LOAD_CLANS,
  LOAD_CLANS_SUCCESS,
  LOAD_CLANS_FAIL,
  LOAD_CLAN,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  ADD_CLAN,
  ADD_CLAN_SUCCESS,
  ADD_CLAN_FAIL,
  DOWNLOAD_CLAN_LOGO,
  DOWNLOAD_CLAN_LOGO_SUCCESS,
  DOWNLOAD_CLAN_LOGO_FAIL,
  UPLOAD_CLAN_LOGO,
  UPLOAD_CLAN_LOGO_SUCCESS,
  UPLOAD_CLAN_LOGO_FAIL
} from "./types";

describe("clans", () => {
  it("should create an action to get all clans", () => {
    const expectedType = [LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/organizations/clans/api/clans";

    const actionCreator = loadClans();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific clan", () => {
    const clanId = "0";

    const expectedType = [LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}`;

    const actionCreator = loadClan(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to create a clan", () => {
    const data = { name: "clanName" };

    const expectedType = [ADD_CLAN, ADD_CLAN_SUCCESS, ADD_CLAN_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/organizations/clans/api/clans";

    const actionCreator = createClan(data);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(data);
  });

  it("should create an action to get a specific clan photo", () => {
    const clanId = "0";

    const expectedType = [DOWNLOAD_CLAN_LOGO, DOWNLOAD_CLAN_LOGO_SUCCESS, DOWNLOAD_CLAN_LOGO_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}/logo`;

    const actionCreator = downloadClanLogo(clanId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to update a clan logo", () => {
    const clanId = "0";
    const data = { logo: "data" };

    const expectedType = [UPLOAD_CLAN_LOGO, UPLOAD_CLAN_LOGO_SUCCESS, UPLOAD_CLAN_LOGO_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = `/organizations/clans/api/clans/${clanId}/logo`;

    const actionCreator = uploadClanLogo(clanId, data);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual(data);
  });
});
