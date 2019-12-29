import { AxiosState } from "utils/axios/types";
import { UserTransaction } from "types";

export type UserTransactionHistoryState = AxiosState<UserTransaction[]>;
