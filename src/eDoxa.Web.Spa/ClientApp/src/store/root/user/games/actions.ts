import {
  LOAD_GAME_CREDENTIAL,
  LOAD_GAME_CREDENTIAL_SUCCESS,
  LOAD_GAME_CREDENTIAL_FAIL,
  LINK_GAME_CREDENTIAL,
  LINK_GAME_CREDENTIAL_SUCCESS,
  LINK_GAME_CREDENTIAL_FAIL,
  UNLINK_GAME_CREDENTIAL,
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL,
  GameCredentialsActionCreators
} from "./types";
import { Game } from "types";

export function loadGameCredential(game: Game): GameCredentialsActionCreators {
  return {
    types: [LOAD_GAME_CREDENTIAL, LOAD_GAME_CREDENTIAL_SUCCESS, LOAD_GAME_CREDENTIAL_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `games/api/${game}/credential`
      }
    }
  };
}

export function linkGameCredential(game: Game): GameCredentialsActionCreators {
  return {
    types: [LINK_GAME_CREDENTIAL, LINK_GAME_CREDENTIAL_SUCCESS, LINK_GAME_CREDENTIAL_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `games/api/${game}/credential`
      }
    }
  };
}

export function unlinkGameCredential(game: Game): GameCredentialsActionCreators {
  return {
    types: [UNLINK_GAME_CREDENTIAL, UNLINK_GAME_CREDENTIAL_SUCCESS, UNLINK_GAME_CREDENTIAL_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `games/api/${game}/credential`
      }
    }
  };
}
