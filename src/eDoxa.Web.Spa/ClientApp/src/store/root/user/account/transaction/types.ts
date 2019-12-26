import { AxiosState } from "utils/axios/types";
import { Transaction } from "types";

export type UserAccountTransactionsState = AxiosState<Transaction[]>;
