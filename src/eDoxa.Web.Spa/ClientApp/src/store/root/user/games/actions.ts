import { LOAD_USER_GAMES, LOAD_USER_GAMES_SUCCESS, LOAD_USER_GAMES_FAIL, UserGamesActionCreators } from "./types";

export function loadGames(): UserGamesActionCreators {
  return {
    types: [LOAD_USER_GAMES, LOAD_USER_GAMES_SUCCESS, LOAD_USER_GAMES_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/games"
      }
    }
  };
}
