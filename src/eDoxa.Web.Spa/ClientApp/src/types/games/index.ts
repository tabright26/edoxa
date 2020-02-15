import { UserId } from "types/identity";

export const GAME_LEAGUEOFLEGENDS = "LeagueOfLegends";

export type Game = typeof GAME_LEAGUEOFLEGENDS;

export type GameServiceName = "Game" | "Challenge" | "Tournament";

export interface GameOptions {
  readonly name: Game;
  readonly displayName: string;
  readonly disabled: boolean;
  readonly services: GameServiceOptions[];
}

export interface GameServiceOptions {
  readonly name: GameServiceName;
  readonly disabled: boolean;
  readonly instructions: string;
}

export interface GameCredential {
  readonly userId: UserId;
  readonly game: Game;
}
