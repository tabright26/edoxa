import {
  LOAD_GAMES,
  LOAD_GAMES_SUCCESS,
  LOAD_GAMES_FAIL,
  GENERATE_GAME_AUTHENTICATION,
  GENERATE_GAME_AUTHENTICATION_SUCCESS,
  GENERATE_GAME_AUTHENTICATION_FAIL,
  VALIDATE_GAME_AUTHENTICATION,
  VALIDATE_GAME_AUTHENTICATION_SUCCESS,
  VALIDATE_GAME_AUTHENTICATION_FAIL,
  UNLINK_GAME_CREDENTIAL,
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL,
  GamesActionCreators,
  GameAuthenticationActionCreators,
  GameCredentialActionCreators
} from "./types";

import { Game } from "types";

export function loadGames(): GamesActionCreators {
  return {
    types: [LOAD_GAMES, LOAD_GAMES_SUCCESS, LOAD_GAMES_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `games/api/games`
      }
    }
  };
}

export function generateGameAuthentication(
  game: Game,
  data: any
): GameAuthenticationActionCreators {
  return {
    types: [
      GENERATE_GAME_AUTHENTICATION,
      GENERATE_GAME_AUTHENTICATION_SUCCESS,
      GENERATE_GAME_AUTHENTICATION_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: `games/api/games/${game}/authentications`,
        data
      }
    }
  };
}

export function validateGameAuthentication(
  game: Game
): GameAuthenticationActionCreators {
  return {
    types: [
      VALIDATE_GAME_AUTHENTICATION,
      VALIDATE_GAME_AUTHENTICATION_SUCCESS,
      VALIDATE_GAME_AUTHENTICATION_FAIL
    ],
    payload: {
      request: {
        method: "PUT",
        url: `games/api/games/${game}/authentications`
      }
    }
  };
}

export function unlinkGameCredential(game: Game): GameCredentialActionCreators {
  return {
    types: [
      UNLINK_GAME_CREDENTIAL,
      UNLINK_GAME_CREDENTIAL_SUCCESS,
      UNLINK_GAME_CREDENTIAL_FAIL
    ],
    payload: {
      request: {
        method: "DELETE",
        url: `games/api/games/${game}/credentials`
      }
    }
  };
}
