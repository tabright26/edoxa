import {
  IdentityStaticOptions,
  CashierStaticOptions,
  ChallengesStaticOptions,
  GamesStaticOptions
} from "types";

export interface StaticOptionsState {
  readonly challenges: ChallengesStaticOptions;
  readonly games: GamesStaticOptions;
  readonly identity: IdentityStaticOptions;
  readonly cashier: CashierStaticOptions;
}
