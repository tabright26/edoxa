import { AxiosState } from "utils/axios/types";
import { StripeCustomer } from "types/payment";

export type StripeCustomerState = AxiosState<StripeCustomer>;
