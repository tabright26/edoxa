import * as Stripe from "stripe";
import { AxiosError } from "axios";

export const selectHasAccountVerified = (account: Stripe.accounts.IAccount, error?: AxiosError | string): boolean => !(!error && account && account.requirements.currently_due.length) || false;
