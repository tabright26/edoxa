import { GENERATE_GAME_AUTH_FACTOR, GENERATE_GAME_AUTH_FACTOR_SUCCESS, GENERATE_GAME_AUTH_FACTOR_FAIL, GameAuthFactorActionCreators } from "./types";
import { Game } from "types";

export function generateGameAuthFactor(game: Game, data: any): GameAuthFactorActionCreators {
  return {
    types: [GENERATE_GAME_AUTH_FACTOR, GENERATE_GAME_AUTH_FACTOR_SUCCESS, GENERATE_GAME_AUTH_FACTOR_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `games/api/${game}/auth-factor`,
        data
      }
    }
  };
}
