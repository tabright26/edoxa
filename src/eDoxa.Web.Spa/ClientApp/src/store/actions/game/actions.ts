import {
  LOAD_GAMES,
  LOAD_GAMES_SUCCESS,
  LOAD_GAMES_FAIL,
  GamesActionCreators
} from "./types";

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
