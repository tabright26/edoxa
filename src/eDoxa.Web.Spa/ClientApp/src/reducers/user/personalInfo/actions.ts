import {
  LOAD_PERSONAL_INFO,
  LOAD_PERSONAL_INFO_SUCCESS,
  LOAD_PERSONAL_INFO_FAIL,
  CREATE_PERSONAL_INFO,
  CREATE_PERSONAL_INFO_SUCCESS,
  CREATE_PERSONAL_INFO_FAIL,
  UPDATE_PERSONAL_INFO,
  UPDATE_PERSONAL_INFO_SUCCESS,
  UPDATE_PERSONAL_INFO_FAIL,
  PersonalInfoActionCreators
} from "./types";

export function loadPersonalInfo(): PersonalInfoActionCreators {
  return {
    types: [LOAD_PERSONAL_INFO, LOAD_PERSONAL_INFO_SUCCESS, LOAD_PERSONAL_INFO_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/personal-info"
      }
    }
  };
}

export function createPersonalInfo(data: any): PersonalInfoActionCreators {
  return {
    types: [CREATE_PERSONAL_INFO, CREATE_PERSONAL_INFO_SUCCESS, CREATE_PERSONAL_INFO_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/personal-info",
        data
      }
    }
  };
}

export function updatePersonalInfo(data: any): PersonalInfoActionCreators {
  return {
    types: [UPDATE_PERSONAL_INFO, UPDATE_PERSONAL_INFO_SUCCESS, UPDATE_PERSONAL_INFO_FAIL],
    payload: {
      request: {
        method: "PUT",
        url: "/identity/api/personal-info",
        data
      }
    }
  };
}
