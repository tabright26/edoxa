import {
  LOAD_PAYMENTMETHODS,
  LOAD_PAYMENTMETHODS_SUCCESS,
  LOAD_PAYMENTMETHODS_FAIL,
  ATTACH_PAYMENTMETHOD,
  ATTACH_PAYMENTMETHOD_SUCCESS,
  ATTACH_PAYMENTMETHOD_FAIL,
  DETACH_PAYMENTMETHOD,
  DETACH_PAYMENTMETHOD_SUCCESS,
  DETACH_PAYMENTMETHOD_FAIL,
  UPDATE_PAYMENTMETHOD,
  UPDATE_PAYMENTMETHOD_SUCCESS,
  UPDATE_PAYMENTMETHOD_FAIL,
  CARD_PAYMENTMETHOD_TYPE,
  PaymentMethodsActionCreators,
  PaymentMethodType
} from "./types";

export function loadPaymentMethods(type: PaymentMethodType = CARD_PAYMENTMETHOD_TYPE): PaymentMethodsActionCreators {
  return {
    types: [LOAD_PAYMENTMETHODS, LOAD_PAYMENTMETHODS_SUCCESS, LOAD_PAYMENTMETHODS_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/payment/api/stripe/payment-methods?type=${type}`
      }
    }
  };
}

export function updatePaymentMethod(paymentMethodId: string, exp_month: number, exp_year: number): PaymentMethodsActionCreators {
  return {
    types: [UPDATE_PAYMENTMETHOD, UPDATE_PAYMENTMETHOD_SUCCESS, UPDATE_PAYMENTMETHOD_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/payment/api/stripe/payment-methods/${paymentMethodId}`,
        data: {
          expMonth: exp_month,
          expYear: exp_year
        }
      }
    }
  };
}

export function attachPaymentMethod(paymentMethodId: string): PaymentMethodsActionCreators {
  return {
    types: [ATTACH_PAYMENTMETHOD, ATTACH_PAYMENTMETHOD_SUCCESS, ATTACH_PAYMENTMETHOD_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/payment/api/stripe/payment-methods/${paymentMethodId}/attach`
      }
    }
  };
}

export function detachPaymentMethod(paymentMethodId: string): PaymentMethodsActionCreators {
  return {
    types: [DETACH_PAYMENTMETHOD, DETACH_PAYMENTMETHOD_SUCCESS, DETACH_PAYMENTMETHOD_FAIL],
    payload: {
      request: {
        method: "POST",
        url: `/payment/api/stripe/payment-methods/${paymentMethodId}/detach`
      }
    }
  };
}
