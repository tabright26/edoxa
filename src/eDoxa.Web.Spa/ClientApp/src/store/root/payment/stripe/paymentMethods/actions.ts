import {
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
  STRIPE_PAYMENTMETHOD_CARD_TYPE,
  StripePaymentMethodsActionCreators,
  StripePaymentMethodType
} from "./types";

export function loadStripePaymentMethods(type: StripePaymentMethodType = STRIPE_PAYMENTMETHOD_CARD_TYPE): StripePaymentMethodsActionCreators {
  return {
    types: [LOAD_STRIPE_PAYMENTMETHODS, LOAD_STRIPE_PAYMENTMETHODS_SUCCESS, LOAD_STRIPE_PAYMENTMETHODS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/payment/api/stripe/payment-methods?type=${type}`
      }
    }
  };
}

export function updateStripePaymentMethod(paymentMethodId: string, exp_month: number, exp_year: number): StripePaymentMethodsActionCreators {
  return {
    types: [UPDATE_STRIPE_PAYMENTMETHOD, UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS, UPDATE_STRIPE_PAYMENTMETHOD_FAIL],
    payload: {
      request: {
        method: "PUT",
        url: `/payment/api/stripe/payment-methods/${paymentMethodId}`,
        data: {
          expMonth: exp_month,
          expYear: exp_year
        }
      }
    }
  };
}

export function attachStripePaymentMethod(paymentMethodId: string): StripePaymentMethodsActionCreators {
  return {
    types: [ATTACH_STRIPE_PAYMENTMETHOD, ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS, ATTACH_STRIPE_PAYMENTMETHOD_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/payment/api/stripe/payment-methods/${paymentMethodId}/attach`
      }
    }
  };
}

export function detachStripePaymentMethod(paymentMethodId: string): StripePaymentMethodsActionCreators {
  return {
    types: [DETACH_STRIPE_PAYMENTMETHOD, DETACH_STRIPE_PAYMENTMETHOD_SUCCESS, DETACH_STRIPE_PAYMENTMETHOD_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/payment/api/stripe/payment-methods/${paymentMethodId}/detach`
      }
    }
  };
}
