import { AxiosState } from "utils/axios/types";
import { StripePaymentMethod } from "types";

export type StripePaymentMethodsState = AxiosState<StripePaymentMethod[]>;
