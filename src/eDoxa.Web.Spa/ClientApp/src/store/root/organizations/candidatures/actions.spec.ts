import { loadCandidatures, loadCandidature, addCandidature, acceptCandidature, declineCandidature } from "./actions";
import {
  LOAD_CANDIDATURES,
  LOAD_CANDIDATURES_SUCCESS,
  LOAD_CANDIDATURES_FAIL,
  LOAD_CANDIDATURE,
  LOAD_CANDIDATURE_SUCCESS,
  LOAD_CANDIDATURE_FAIL,
  ADD_CANDIDATURE,
  ADD_CANDIDATURE_SUCCESS,
  ADD_CANDIDATURE_FAIL,
  ACCEPT_CANDIDATURE,
  ACCEPT_CANDIDATURE_SUCCESS,
  ACCEPT_CANDIDATURE_FAIL,
  DECLINE_CANDIDATURE,
  DECLINE_CANDIDATURE_SUCCESS,
  DECLINE_CANDIDATURE_FAIL
} from "./types";

describe("candidatures", () => {
  it("should create an action to get user candidatures", () => {
    const type = "user";
    const id = "0";

    const expectedType = [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/candidatures?${type}Id=${id}`;

    const actionCreator = loadCandidatures(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get clan candidatures", () => {
    const type = "clan";
    const id = "100";

    const expectedType = [LOAD_CANDIDATURES, LOAD_CANDIDATURES_SUCCESS, LOAD_CANDIDATURES_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/candidatures?${type}Id=${id}`;

    const actionCreator = loadCandidatures(type, id);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a specific candidature", () => {
    const candidatureId = "10";

    const expectedType = [LOAD_CANDIDATURE, LOAD_CANDIDATURE_SUCCESS, LOAD_CANDIDATURE_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/organizations/clans/api/candidatures/${candidatureId}`;

    const actionCreator = loadCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to add a candidature", () => {
    const clanId = "10";
    const userId = "10";

    const expectedType = [ADD_CANDIDATURE, ADD_CANDIDATURE_SUCCESS, ADD_CANDIDATURE_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/organizations/clans/api/candidatures";

    const actionCreator = addCandidature(clanId, userId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
    expect(actionCreator.payload.request.data).toEqual({ clanId: clanId, userId: userId });
  });

  it("should create an action to accept a candidature", () => {
    const candidatureId = "10";

    const expectedType = [ACCEPT_CANDIDATURE, ACCEPT_CANDIDATURE_SUCCESS, ACCEPT_CANDIDATURE_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = `/organizations/clans/api/candidatures/${candidatureId}`;

    const actionCreator = acceptCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to decline a candidature", () => {
    const candidatureId = "10";

    const expectedType = [DECLINE_CANDIDATURE, DECLINE_CANDIDATURE_SUCCESS, DECLINE_CANDIDATURE_FAIL];
    const expectedMethod = "DELETE";
    const expectedUrl = `/organizations/clans/api/candidatures/${candidatureId}`;

    const actionCreator = declineCandidature(candidatureId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
