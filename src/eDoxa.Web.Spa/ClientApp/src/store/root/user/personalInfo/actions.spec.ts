import {
  LOAD_PERSONAL_INFO,
  LOAD_PERSONAL_INFO_SUCCESS,
  LOAD_PERSONAL_INFO_FAIL,
  CREATE_PERSONAL_INFO,
  CREATE_PERSONAL_INFO_SUCCESS,
  CREATE_PERSONAL_INFO_FAIL,
  UPDATE_PERSONAL_INFO,
  UPDATE_PERSONAL_INFO_SUCCESS,
  UPDATE_PERSONAL_INFO_FAIL
} from "./types";
import { loadPersonalInfo, createPersonalInfo, updatePersonalInfo } from "./actions";

describe("identity actions", () => {
  it("should create an action to get user personal info", () => {
    const expectedType = [LOAD_PERSONAL_INFO, LOAD_PERSONAL_INFO_SUCCESS, LOAD_PERSONAL_INFO_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/personal-info";

    const actionCreator = loadPersonalInfo();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to post user personal info", () => {
    const expectedType = [CREATE_PERSONAL_INFO, CREATE_PERSONAL_INFO_SUCCESS, CREATE_PERSONAL_INFO_FAIL];
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
    const expectedType = [UPDATE_PERSONAL_INFO, UPDATE_PERSONAL_INFO_SUCCESS, UPDATE_PERSONAL_INFO_FAIL];
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
