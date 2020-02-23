import { AxiosState } from "utils/axios/types";
import { Transaction } from "types/cashier";

export type UserTransactionState = AxiosState<Transaction[]>;
