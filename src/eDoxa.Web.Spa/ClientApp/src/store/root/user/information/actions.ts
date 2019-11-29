import {
  LOAD_USER_INFORMATIONS,
  LOAD_USER_INFORMATIONS_SUCCESS,
  LOAD_USER_INFORMATIONS_FAIL,
  CREATE_USER_INFORMATIONS,
  CREATE_USER_INFORMATIONS_SUCCESS,
  CREATE_USER_INFORMATIONS_FAIL,
  UPDATE_USER_INFORMATIONS,
  UPDATE_USER_INFORMATIONS_SUCCESS,
  UPDATE_USER_INFORMATIONS_FAIL,
  UserInformationsActionCreators
} from "./types";

export function loadUserInformations(): UserInformationsActionCreators {
  return {
    types: [
      LOAD_USER_INFORMATIONS,
      LOAD_USER_INFORMATIONS_SUCCESS,
      LOAD_USER_INFORMATIONS_FAIL
    ],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/informations"
      }
    }
  };
}

export function createUserInformations(
  data: any,
  meta: any
): UserInformationsActionCreators | any {
  return {
    types: [
      CREATE_USER_INFORMATIONS,
      CREATE_USER_INFORMATIONS_SUCCESS,
      CREATE_USER_INFORMATIONS_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/informations",
        data
      }
    },
    meta
  };
}

export function updateUserInformations(
  data: any,
  meta: any
): UserInformationsActionCreators {
  return {
    types: [
      UPDATE_USER_INFORMATIONS,
      UPDATE_USER_INFORMATIONS_SUCCESS,
      UPDATE_USER_INFORMATIONS_FAIL
    ],
    payload: {
      request: {
        method: "PUT",
        url: "/identity/api/informations",
        data: {
          firstName: data.firstName
        }
      }
    },
    meta
  };
}
