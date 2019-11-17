import {
  GENERATE_GAME_AUTHENTICATION,
  GENERATE_GAME_AUTHENTICATION_SUCCESS,
  GENERATE_GAME_AUTHENTICATION_FAIL,
  VALIDATE_GAME_AUTHENTICATION,
  VALIDATE_GAME_AUTHENTICATION_SUCCESS,
  VALIDATE_GAME_AUTHENTICATION_FAIL,
  GameAuthenticationActionCreators
} from "./types";
import { Game } from "types";

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
