import {
  IdentityStaticOptions,
  PaymentStaticOptions,
  TransactionBundle
} from "types";

export interface StaticOptionsState {
  readonly identity: IdentityStaticOptions;
  readonly payment: PaymentStaticOptions;
  readonly transactionBundles: TransactionBundle[];
}
