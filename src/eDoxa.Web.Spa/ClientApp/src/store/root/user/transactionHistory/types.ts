import { AxiosState } from "utils/axios/types";
import { Transaction } from "types";

export type UserTransactionState = AxiosState<Transaction[]>;
