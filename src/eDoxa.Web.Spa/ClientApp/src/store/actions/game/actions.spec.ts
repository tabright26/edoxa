import { loadGames } from "./actions";
import {
  LOAD_GAMES,
  LOAD_GAMES_SUCCESS,
  LOAD_GAMES_FAIL,
  LoadGamesType,
  GamesActionCreators
} from "./types";

describe("identity actions", () => {
  it("should create an action to get user games id", () => {
    const expectedType: LoadGamesType[] = [
      LOAD_GAMES,
      LOAD_GAMES_SUCCESS,
      LOAD_GAMES_FAIL
    ];
    const expectedMethod = "GET";
    const expectedUrl = "games/api/games";

    const object: GamesActionCreators = loadGames();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
