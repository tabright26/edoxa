import {
  UNLINK_GAME_CREDENTIAL,
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL,
  GameCredentialActionCreators
} from "./types";
import { Game } from "types";

export function unlinkGameAccountCredential(
  game: Game
): GameCredentialActionCreators {
  return {
    types: [
      UNLINK_GAME_CREDENTIAL,
      UNLINK_GAME_CREDENTIAL_SUCCESS,
      UNLINK_GAME_CREDENTIAL_FAIL
    ],
    payload: {
      request: {
        method: "DELETE",
        url: `games/api/${game}/credential`
      }
    }
  };
}
