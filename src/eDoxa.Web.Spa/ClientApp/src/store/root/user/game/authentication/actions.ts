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

export function generateGameAccountAuthentication(
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
        url: `games/api/${game}/auth-factor`,
        data
      }
    }
  };
}

export function validateGameAccountAuthentication(
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
        method: "POST",
        url: `games/api/${game}/credential`
      }
    }
  };
}
