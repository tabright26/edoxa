import {
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
  UpdateStripeCustomerDefaultPaymentMethodActionCreator,
  LoadStripePaymentMethodsActionCreator,
  UpdateStripePaymentMethodActionCreator,
  AttachStripePaymentMethodActionCreator,
  DetachStripePaymentMethodActionCreator
} from "./types";

import {
  AXIOS_PAYLOAD_CLIENT_CASHIER,
  AxiosActionCreatorMeta
} from "utils/axios/types";
import { StripePaymentMethodId } from "types/payment";

export function updateStripeCustomerDefaultPaymentMethod(
  paymentMethodId: StripePaymentMethodId
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
  defaultPaymentMethod: boolean = false,
  meta: AxiosActionCreatorMeta
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
    },
    meta
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
