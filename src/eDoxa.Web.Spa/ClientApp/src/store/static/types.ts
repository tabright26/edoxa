import {
  IdentityStaticOptions,
  PaymentStaticOptions,
  CashierStaticOptions
} from "types";

export interface StaticOptionsState {
  readonly identity: IdentityStaticOptions;
  readonly payment: PaymentStaticOptions;
  readonly cashier: CashierStaticOptions;
}
