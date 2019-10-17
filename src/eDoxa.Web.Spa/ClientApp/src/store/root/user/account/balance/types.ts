import { UserMoneyAccountBalanceState, LoadUserMoneyAccountBalanceActionCreator } from "./money/types";
import { UserTokenAccountBalanceState, LoadUserTokenAccountBalanceActionCreator } from "./token/types";

export type UserAccountBalanceActionCreators = LoadUserMoneyAccountBalanceActionCreator | LoadUserTokenAccountBalanceActionCreator;
export type UserAccountBalanceState = UserMoneyAccountBalanceState | UserTokenAccountBalanceState;
