import * as Stripe from "stripe";
import { AxiosError } from "axios";

export const selectHasDefaultPaymentMethod = (customer: Stripe.customers.ICustomer, error?: AxiosError | string): boolean =>
  !(!error && customer && !customer.invoice_settings.default_payment_method) || false;
