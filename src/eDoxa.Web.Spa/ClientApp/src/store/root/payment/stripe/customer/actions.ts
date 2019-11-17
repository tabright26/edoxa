import {
  LOAD_STRIPE_CUSTOMER,
  LOAD_STRIPE_CUSTOMER_SUCCESS,
  LOAD_STRIPE_CUSTOMER_FAIL,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL,
  StripeCustomerActionCreators
} from "./types";

export function loadStripeCustomer(): StripeCustomerActionCreators {
  return {
    types: [LOAD_STRIPE_CUSTOMER, LOAD_STRIPE_CUSTOMER_SUCCESS, LOAD_STRIPE_CUSTOMER_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/payment/api/stripe/customer"
      }
    }
  };
}

export function updateStripeCustomerDefaultPaymentMethod(paymentMethodId): StripeCustomerActionCreators {
  return {
    types: [UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD, UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS, UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL],
    payload: {
      request: {
        method: "PUT",
        url: `/payment/api/stripe/customer/payment-methods/${paymentMethodId}/default`
      }
    }
  };
}
