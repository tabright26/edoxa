import {
  GENERATE_GAME_AUTHENTICATION,
  GENERATE_GAME_AUTHENTICATION_SUCCESS,
  GENERATE_GAME_AUTHENTICATION_FAIL,
  VALIDATE_GAME_AUTHENTICATION,
  VALIDATE_GAME_AUTHENTICATION_SUCCESS,
  VALIDATE_GAME_AUTHENTICATION_FAIL,
  UNLINK_GAME_CREDENTIAL,
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL,
  GenerateGameAuthenticationActionCreator,
  ValidateGameAuthenticationActionCreator,
  UnlinkGameCredentialActionCreator
} from "./types";
import {
  AXIOS_PAYLOAD_CLIENT_DEFAULT,
  AxiosActionCreatorMeta
} from "utils/axios/types";
import { Game } from "types/games";

export function generateGameAuthentication(
  game: Game,
  data: any,
  meta: AxiosActionCreatorMeta
): GenerateGameAuthenticationActionCreator {
  return {
    types: [
      GENERATE_GAME_AUTHENTICATION,
      GENERATE_GAME_AUTHENTICATION_SUCCESS,
      GENERATE_GAME_AUTHENTICATION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "POST",
        url: `games/api/games/${game}/authentications`,
        data
      }
    },
    meta
  };
}

export function validateGameAuthentication(
  game: Game,
  meta: AxiosActionCreatorMeta
): ValidateGameAuthenticationActionCreator {
  return {
    types: [
      VALIDATE_GAME_AUTHENTICATION,
      VALIDATE_GAME_AUTHENTICATION_SUCCESS,
      VALIDATE_GAME_AUTHENTICATION_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "PUT",
        url: `games/api/games/${game}/authentications`
      }
    },
    meta
  };
}

export function unlinkGameCredential(
  game: Game,
  meta: AxiosActionCreatorMeta
): UnlinkGameCredentialActionCreator {
  return {
    types: [
      UNLINK_GAME_CREDENTIAL,
      UNLINK_GAME_CREDENTIAL_SUCCESS,
      UNLINK_GAME_CREDENTIAL_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "DELETE",
        url: `games/api/games/${game}/credentials`
      }
    },
    meta
  };
}
