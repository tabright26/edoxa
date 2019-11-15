import { loadClanCandidatures, loadClanCandidature, sendClanCandidature, acceptClanCandidature, declineClanCandidature } from "./actions";
import {
  LOAD_CLAN_CANDIDATURES,
  LOAD_CLAN_CANDIDATURES_SUCCESS,
  LOAD_CLAN_CANDIDATURES_FAIL,
  LOAD_CLAN_CANDIDATURE,
  LOAD_CLAN_CANDIDATURE_SUCCESS,
  LOAD_CLAN_CANDIDATURE_FAIL,
  SEND_CLAN_CANDIDATURE,
  SEND_CLAN_CANDIDATURE_SUCCESS,
  SEND_CLAN_CANDIDATURE_FAIL,
  ACCEPT_CLAN_CANDIDATURE,
  ACCEPT_CLAN_CANDIDATURE_SUCCESS,
  ACCEPT_CLAN_CANDIDATURE_FAIL,
  REFUSE_CLAN_CANDIDATURE,
  REFUSE_CLAN_CANDIDATURE_SUCCESS,
  REFUSE_CLAN_CANDIDATURE_FAIL
} from "./types";

describe("candidatures", () => {
  it("should create an action to get user candidatures", () => {
    const type = "user";
    const id = "0";

    const expectedType = [LOAD_CLAN_CANDIDATURES, LOAD_CLAN_CANDIDATURES_SUCCESS, LOAD_CLAN_CANDIDATURES_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/candidatures?${type}Id=${id}`;

    const actionCreator = loadClanCandidatures(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get clan candidatures", () => {
    const type = "clan";
    const id = "100";

    const expectedType = [LOAD_CLAN_CANDIDATURES, LOAD_CLAN_CANDIDATURES_SUCCESS, LOAD_CLAN_CANDIDATURES_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/candidatures?${type}Id=${id}`;

    const actionCreator = loadClanCandidatures(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific candidature", () => {
    const candidatureId = "10";

    const expectedType = [LOAD_CLAN_CANDIDATURE, LOAD_CLAN_CANDIDATURE_SUCCESS, LOAD_CLAN_CANDIDATURE_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/clans/api/candidatures/${candidatureId}`;

    const actionCreator = loadClanCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to add a candidature", () => {
    const clanId = "10";
    const userId = "10";

    const expectedType = [SEND_CLAN_CANDIDATURE, SEND_CLAN_CANDIDATURE_SUCCESS, SEND_CLAN_CANDIDATURE_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/clans/api/candidatures";

    const actionCreator = sendClanCandidature(clanId, userId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual({ clanId: clanId, userId: userId });
  });

  it("should create an action to accept a candidature", () => {
    const candidatureId = "10";

    const expectedType = [ACCEPT_CLAN_CANDIDATURE, ACCEPT_CLAN_CANDIDATURE_SUCCESS, ACCEPT_CLAN_CANDIDATURE_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = `/clans/api/candidatures/${candidatureId}`;

    const actionCreator = acceptClanCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to decline a candidature", () => {
    const candidatureId = "10";

    const expectedType = [REFUSE_CLAN_CANDIDATURE, REFUSE_CLAN_CANDIDATURE_SUCCESS, REFUSE_CLAN_CANDIDATURE_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/clans/api/candidatures/${candidatureId}`;

    const actionCreator = declineClanCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
