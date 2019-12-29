import { AxiosState } from "utils/axios/types";

import { TransactionBundle } from "types";

export type UserAccountWithdrawalBundlesState = AxiosState<TransactionBundle[]>;
