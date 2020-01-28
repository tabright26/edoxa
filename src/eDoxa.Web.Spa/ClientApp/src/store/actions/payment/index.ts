import {
  LOAD_STRIPE_ACCOUNT,
  LOAD_STRIPE_ACCOUNT_SUCCESS,
  LOAD_STRIPE_ACCOUNT_FAIL,
  LOAD_STRIPE_BANKACCOUNT,
  LOAD_STRIPE_BANKACCOUNT_SUCCESS,
  LOAD_STRIPE_BANKACCOUNT_FAIL,
  UPDATE_STRIPE_BANKACCOUNT,
  UPDATE_STRIPE_BANKACCOUNT_SUCCESS,
  UPDATE_STRIPE_BANKACCOUNT_FAIL,
  LOAD_STRIPE_CUSTOMER,
  LOAD_STRIPE_CUSTOMER_SUCCESS,
  LOAD_STRIPE_CUSTOMER_FAIL,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL,
  LOAD_STRIPE_PAYMENTMETHODS,
  LOAD_STRIPE_PAYMENTMETHODS_SUCCESS,
  LOAD_STRIPE_PAYMENTMETHODS_FAIL,
  ATTACH_STRIPE_PAYMENTMETHOD,
  ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS,
  ATTACH_STRIPE_PAYMENTMETHOD_FAIL,
  DETACH_STRIPE_PAYMENTMETHOD,
  DETACH_STRIPE_PAYMENTMETHOD_SUCCESS,
  DETACH_STRIPE_PAYMENTMETHOD_FAIL,
  UPDATE_STRIPE_PAYMENTMETHOD,
  UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS,
  UPDATE_STRIPE_PAYMENTMETHOD_FAIL,
  LoadStripeAccountActionCreator,
  LoadStripeBankAccountActionCreator,
  UpdateStripeBankAccountActionCreator,
  LoadStripeCustomerActionCreator,
  UpdateStripeCustomerDefaultPaymentMethodActionCreator,
  LoadStripePaymentMethodsActionCreator,
  UpdateStripePaymentMethodActionCreator,
  AttachStripePaymentMethodActionCreator,
  DetachStripePaymentMethodActionCreator
} from "./types";

import { AXIOS_PAYLOAD_CLIENT_CASHIER } from "utils/axios/types";

export function loadStripeAccount(): LoadStripeAccountActionCreator {
  return {
    types: [
      LOAD_STRIPE_ACCOUNT,
      LOAD_STRIPE_ACCOUNT_SUCCESS,
      LOAD_STRIPE_ACCOUNT_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "GET",
        url: "/payment/api/stripe/account"
      }
    }
  };
}

export function loadStripeBankAccount(): LoadStripeBankAccountActionCreator {
  return {
    types: [
      LOAD_STRIPE_BANKACCOUNT,
      LOAD_STRIPE_BANKACCOUNT_SUCCESS,
      LOAD_STRIPE_BANKACCOUNT_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "GET",
        url: "/payment/api/stripe/bank-account"
      }
    }
  };
}

export function updateStripeBankAccount(
  token: stripe.Token
): UpdateStripeBankAccountActionCreator {
  return {
    types: [
      UPDATE_STRIPE_BANKACCOUNT,
      UPDATE_STRIPE_BANKACCOUNT_SUCCESS,
      UPDATE_STRIPE_BANKACCOUNT_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "POST",
        url: "/payment/api/stripe/bank-account",
        data: {
          token: token.id
        }
      }
    }
  };
}

export function loadStripeCustomer(): LoadStripeCustomerActionCreator {
  return {
    types: [
      LOAD_STRIPE_CUSTOMER,
      LOAD_STRIPE_CUSTOMER_SUCCESS,
      LOAD_STRIPE_CUSTOMER_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "GET",
        url: "/payment/api/stripe/customer"
      }
    }
  };
}

export function updateStripeCustomerDefaultPaymentMethod(
  paymentMethodId
): UpdateStripeCustomerDefaultPaymentMethodActionCreator {
  return {
    types: [
      UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD,
      UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS,
      UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "PUT",
        url: `/payment/api/stripe/customer/payment-methods/${paymentMethodId}/default`
      }
    }
  };
}

export function loadStripePaymentMethods(): LoadStripePaymentMethodsActionCreator {
  return {
    types: [
      LOAD_STRIPE_PAYMENTMETHODS,
      LOAD_STRIPE_PAYMENTMETHODS_SUCCESS,
      LOAD_STRIPE_PAYMENTMETHODS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "GET",
        url: "/payment/api/stripe/payment-methods"
      }
    }
  };
}

export function updateStripePaymentMethod(
  paymentMethodId: string,
  expMonth: number,
  expYear: number
): UpdateStripePaymentMethodActionCreator {
  return {
    types: [
      UPDATE_STRIPE_PAYMENTMETHOD,
      UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS,
      UPDATE_STRIPE_PAYMENTMETHOD_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "PUT",
        url: `/payment/api/stripe/payment-methods/${paymentMethodId}`,
        data: {
          expMonth,
          expYear
        }
      }
    }
  };
}

export function attachStripePaymentMethod(
  paymentMethod: stripe.paymentMethod.PaymentMethod,
  defaultPaymentMethod: boolean = false
): AttachStripePaymentMethodActionCreator {
  return {
    types: [
      ATTACH_STRIPE_PAYMENTMETHOD,
      ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS,
      ATTACH_STRIPE_PAYMENTMETHOD_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "POST",
        url: `/payment/api/stripe/payment-methods/${paymentMethod.id}/attach`,
        data: {
          defaultPaymentMethod
        }
      }
    }
  };
}

export function detachStripePaymentMethod(
  paymentMethodId: string
): DetachStripePaymentMethodActionCreator {
  return {
    types: [
      DETACH_STRIPE_PAYMENTMETHOD,
      DETACH_STRIPE_PAYMENTMETHOD_SUCCESS,
      DETACH_STRIPE_PAYMENTMETHOD_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "POST",
        url: `/payment/api/stripe/payment-methods/${paymentMethodId}/detach`
      }
    }
  };
}
