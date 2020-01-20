import {
  IdentityStaticOptions,
  PaymentStaticOptions,
  CashierStaticOptions,
  ChallengesStaticOptions,
  GamesStaticOptions
} from "types";

export interface StaticOptionsState {
  readonly challenges: ChallengesStaticOptions;
  readonly games: GamesStaticOptions;
  readonly identity: IdentityStaticOptions;
  readonly payment: PaymentStaticOptions;
  readonly cashier: CashierStaticOptions;
}
