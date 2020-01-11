import { AxiosState } from "utils/axios/types";
import { UserTransaction } from "types";

export type UserTransactionState = AxiosState<UserTransaction[]>;
