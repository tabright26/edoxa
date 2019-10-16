import {
  LOAD_USER_INFORMATIONS,
  LOAD_USER_INFORMATIONS_SUCCESS,
  LOAD_USER_INFORMATIONS_FAIL,
  CREATE_USER_INFORMATIONS,
  CREATE_USER_INFORMATIONS_SUCCESS,
  CREATE_USER_INFORMATIONS_FAIL,
  UPDATE_USER_INFORMATIONS,
  UPDATE_USER_INFORMATIONS_SUCCESS,
  UPDATE_USER_INFORMATIONS_FAIL
} from "./types";
import { loadPersonalInfo, createPersonalInfo, updatePersonalInfo } from "./actions";

describe("identity actions", () => {
  it("should create an action to get user personal info", () => {
    const expectedType = [LOAD_USER_INFORMATIONS, LOAD_USER_INFORMATIONS_SUCCESS, LOAD_USER_INFORMATIONS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/personal-info";

    const actionCreator = loadPersonalInfo();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user personal info", () => {
    const expectedType = [CREATE_USER_INFORMATIONS, CREATE_USER_INFORMATIONS_SUCCESS, CREATE_USER_INFORMATIONS_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/personal-info";
    const expectedPersonalInfo = { firstName: "Bob", lastName: "Afrete" };

    const object = createPersonalInfo(expectedPersonalInfo);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedPersonalInfo);
  });

  it("should create an action to put user doxatag", () => {
    const expectedType = [UPDATE_USER_INFORMATIONS, UPDATE_USER_INFORMATIONS_SUCCESS, UPDATE_USER_INFORMATIONS_FAIL];
    const expectedMethod = "PUT";
    const expectedUrl = "/identity/api/personal-info";
    const expectedPersonalInfo = { firstName: "Bob", lastName: "Afrete" };

    const object = updatePersonalInfo(expectedPersonalInfo);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedPersonalInfo);
  });
});
