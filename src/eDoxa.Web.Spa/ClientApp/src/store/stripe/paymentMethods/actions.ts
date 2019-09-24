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

export function loadPaymentMethods(customer: string, type: PaymentMethodType = CARD_PAYMENTMETHOD_TYPE): PaymentMethodsActionCreators {
  return {
    types: [LOAD_PAYMENTMETHODS, LOAD_PAYMENTMETHODS_SUCCESS, LOAD_PAYMENTMETHODS_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "GET",
        url: `/v1/payment_methods?customer=${customer}&type=${type}`
      }
    }
  };
}

export function attachPaymentMethod(paymentMethodId: string, customer: string): PaymentMethodsActionCreators {
  return {
    types: [ATTACH_PAYMENTMETHOD, ATTACH_PAYMENTMETHOD_SUCCESS, ATTACH_PAYMENTMETHOD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "POST",
        url: `/v1/payment_methods/${paymentMethodId}/attach`,
        data: {
          customer
        }
      }
    }
  };
}

export function detachPaymentMethod(paymentMethodId: string): PaymentMethodsActionCreators {
  return {
    types: [DETACH_PAYMENTMETHOD, DETACH_PAYMENTMETHOD_SUCCESS, DETACH_PAYMENTMETHOD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "POST",
        url: `/v1/payment_methods/${paymentMethodId}/detach`
      }
    }
  };
}

export function updatePaymentMethod(paymentMethodId: string, exp_month: number, exp_year: number): PaymentMethodsActionCreators {
  return {
    types: [UPDATE_PAYMENTMETHOD, UPDATE_PAYMENTMETHOD_SUCCESS, UPDATE_PAYMENTMETHOD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "POST",
        url: `/v1/payment_methods/${paymentMethodId}?card[exp_month]=${exp_month}&card[exp_year]=${exp_year}`
      }
    }
  };
}
