import { AxiosState } from "utils/axios/types";

import { TransactionBundle } from "types";

export type UserAccountDepositBundlesState = AxiosState<TransactionBundle[]>;
