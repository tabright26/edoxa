import { AxiosState } from "utils/axios/types";
import { StripePaymentMethod } from "types/payment";

export type StripePaymentMethodsState = AxiosState<StripePaymentMethod[]>;
