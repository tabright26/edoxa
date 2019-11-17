import { LoadUserMoneyAccountBalanceActionCreator } from "./money/types";
import { LoadUserTokenAccountBalanceActionCreator } from "./token/types";
import { AxiosState } from "utils/axios/types";
import { Balance } from "types";

export type UserAccountBalanceActionCreators = LoadUserMoneyAccountBalanceActionCreator | LoadUserTokenAccountBalanceActionCreator;
export type UserAccountBalanceState = AxiosState<Balance>;
