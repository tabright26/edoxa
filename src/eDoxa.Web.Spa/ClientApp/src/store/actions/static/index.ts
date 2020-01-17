import {
  IdentityStaticOptionsActionCreators,
  LOAD_IDENTITY_STATIC_OPTIONS,
  LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
  LOAD_IDENTITY_STATIC_OPTIONS_FAIL
} from "./types";
import { AXIOS_PAYLOAD_CLIENT_DEFAULT } from "utils/axios/types";

export function loadIdentityStaticOptions(): IdentityStaticOptionsActionCreators {
  return {
    types: [
      LOAD_IDENTITY_STATIC_OPTIONS,
      LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
      LOAD_IDENTITY_STATIC_OPTIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `identity/api/static/options`
      }
    }
  };
}
