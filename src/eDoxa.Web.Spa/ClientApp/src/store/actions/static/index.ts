import {
  LOAD_IDENTITY_STATIC_OPTIONS,
  LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
  LOAD_IDENTITY_STATIC_OPTIONS_FAIL,
  LOAD_PAYMENT_STATIC_OPTIONS,
  LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS,
  LOAD_PAYMENT_STATIC_OPTIONS_FAIL,
  StaticOptionsActionCreators,
  LOAD_CASHIER_STATIC_OPTIONS,
  LOAD_CASHIER_STATIC_OPTIONS_SUCCESS,
  LOAD_CASHIER_STATIC_OPTIONS_FAIL,
  LOAD_GAMES_STATIC_OPTIONS,
  LOAD_GAMES_STATIC_OPTIONS_SUCCESS,
  LOAD_GAMES_STATIC_OPTIONS_FAIL,
  LOAD_CHALLENGES_STATIC_OPTIONS,
  LOAD_CHALLENGES_STATIC_OPTIONS_SUCCESS,
  LOAD_CHALLENGES_STATIC_OPTIONS_FAIL
} from "./types";
import {
  AXIOS_PAYLOAD_CLIENT_DEFAULT,
  AXIOS_PAYLOAD_CLIENT_CASHIER,
  AXIOS_PAYLOAD_CLIENT_CHALLENGES
} from "utils/axios/types";

export function loadIdentityStaticOptions(): StaticOptionsActionCreators {
  return {
    types: [
      LOAD_IDENTITY_STATIC_OPTIONS,
      LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
      LOAD_IDENTITY_STATIC_OPTIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_DEFAULT,
      request: {
        method: "GET",
        url: `identity/api/static/options`
      }
    }
  };
}

export function loadPaymentStaticOptions(): StaticOptionsActionCreators {
  return {
    types: [
      LOAD_PAYMENT_STATIC_OPTIONS,
      LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS,
      LOAD_PAYMENT_STATIC_OPTIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "GET",
        url: "payment/api/static/options"
      }
    }
  };
}

export function loadCashierStaticOptions(): StaticOptionsActionCreators {
  return {
    types: [
      LOAD_CASHIER_STATIC_OPTIONS,
      LOAD_CASHIER_STATIC_OPTIONS_SUCCESS,
      LOAD_CASHIER_STATIC_OPTIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CASHIER,
      request: {
        method: "GET",
        url: "/cashier/api/static/options"
      }
    }
  };
}

export function loadChallengesStaticOptions(): StaticOptionsActionCreators {
  return {
    types: [
      LOAD_CHALLENGES_STATIC_OPTIONS,
      LOAD_CHALLENGES_STATIC_OPTIONS_SUCCESS,
      LOAD_CHALLENGES_STATIC_OPTIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CHALLENGES,
      request: {
        method: "GET",
        url: "/challenges/api/static/options"
      }
    }
  };
}

export function loadGamesStaticOptions(): StaticOptionsActionCreators {
  return {
    types: [
      LOAD_GAMES_STATIC_OPTIONS,
      LOAD_GAMES_STATIC_OPTIONS_SUCCESS,
      LOAD_GAMES_STATIC_OPTIONS_FAIL
    ],
    payload: {
      client: AXIOS_PAYLOAD_CLIENT_CHALLENGES,
      request: {
        method: "GET",
        url: "/games/api/static/options"
      }
    }
  };
}
