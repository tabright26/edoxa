export const LOAD_USER_STRIPE_CARDS = "LOAD_USER_STRIPE_CARDS";
export const LOAD_USER_STRIPE_CARDS_SUCCESS = "LOAD_USER_STRIPE_CARDS_SUCCESS";
export const LOAD_USER_STRIPE_CARDS_FAIL = "LOAD_USER_STRIPE_CARDS_FAIL";
export function loadUserStripeCards() {
  return {
    types: [LOAD_USER_STRIPE_CARDS, LOAD_USER_STRIPE_CARDS_SUCCESS, LOAD_USER_STRIPE_CARDS_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "get",
        url: `/v1/customers/:customerId/sources?object=card`
      }
    }
  };
}

export const LOAD_USER_STRIPE_BANK_ACCOUNTS = "LOAD_USER_STRIPE_BANK_ACCOUNTS";
export const LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS = "LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS";
export const LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL = "LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL";
export function loadUserStripeBankAccounts() {
  return {
    types: [LOAD_USER_STRIPE_BANK_ACCOUNTS, LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS, LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "get",
        url: "/v1/accounts/:connectAccountId/external_accounts?object=bank_account"
      }
    }
  };
}

export const ADD_STRIPE_CREDIT_CARD = "ADD_STRIPE_CREDIT_CARD";
export const ADD_STRIPE_CREDIT_CARD_SUCCESS = "ADD_STRIPE_CREDIT_CARD_SUCCESS";
export const ADD_STRIPE_CREDIT_CARD_FAIL = "ADD_STRIPE_CREDIT_CARD_FAIL";
export function addStripeCreditCard(data) {
  return {
    types: [ADD_STRIPE_CREDIT_CARD, ADD_STRIPE_CREDIT_CARD_SUCCESS, ADD_STRIPE_CREDIT_CARD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "post",
        url: "/v1/customers/:customerId/sources",
        data: {
          source: data
        }
      }
    }
  };
}

export const REMOVE_STRIPE_CREDIT_CARD = "REMOVE_STRIPE_CREDIT_CARD";
export const REMOVE_STRIPE_CREDIT_CARD_SUCCESS = "REMOVE_STRIPE_CREDIT_CARD_SUCCESS";
export const REMOVE_STRIPE_CREDIT_CARD_FAIL = "REMOVE_STRIPE_CREDIT_CARD_FAIL";
export function removeStripeCreditCard(cardId) {
  return {
    types: [REMOVE_STRIPE_CREDIT_CARD, REMOVE_STRIPE_CREDIT_CARD_SUCCESS, REMOVE_STRIPE_CREDIT_CARD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "delete",
        url: `/v1/customers/:customerId/sources/${cardId}`
      }
    }
  };
}

export const UPDATE_STRIPE_CREDIT_CARD = "UPDATE_STRIPE_CREDIT_CARD";
export const UPDATE_STRIPE_CREDIT_CARD_SUCCESS = "UPDATE_STRIPE_CREDIT_CARD_SUCCESS";
export const UPDATE_STRIPE_CREDIT_CARD_FAIL = "UPDATE_STRIPE_CREDIT_CARD_FAIL";
export function updateStripeCreditCard(cardId, data) {
  return {
    types: [UPDATE_STRIPE_CREDIT_CARD, UPDATE_STRIPE_CREDIT_CARD_SUCCESS, UPDATE_STRIPE_CREDIT_CARD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "put",
        url: `/v1/customers/:customerId/sources/${cardId}`,
        data
      }
    }
  };
}

export const ADD_STRIPE_BANK = "ADD_STRIPE_BANK";
export const ADD_STRIPE_BANK_SUCCESS = "ADD_STRIPE_BANK_SUCCESS";
export const ADD_STRIPE_BANK_FAIL = "ADD_STRIPE_BANK_FAIL";
export function addStripeBank(data) {
  return {
    types: [ADD_STRIPE_BANK, ADD_STRIPE_BANK_SUCCESS, ADD_STRIPE_BANK_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "post",
        url: "/v1/accounts/:connectAccountId/external_accounts",
        data: {
          external_account: data
        }
      }
    }
  };
}

export const REMOVE_STRIPE_BANK = "REMOVE_STRIPE_BANK";
export const REMOVE_STRIPE_BANK_SUCCESS = "REMOVE_STRIPE_BANK_SUCCESS";
export const REMOVE_STRIPE_BANK_FAIL = "REMOVE_STRIPE_BANK_FAIL";
export function removeStripeBank(bankId) {
  return {
    types: [REMOVE_STRIPE_BANK, REMOVE_STRIPE_BANK_SUCCESS, REMOVE_STRIPE_BANK_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "delete",
        url: `/v1/customers/:customerId/sources/${bankId}`
      }
    }
  };
}

export const UPDATE_STRIPE_BANK = "UPDATE_STRIPE_BANK";
export const UPDATE_STRIPE_BANK_SUCCESS = "UPDATE_STRIPE_BANK_SUCCESS";
export const UPDATE_STRIPE_BANK_FAIL = "UPDATE_STRIPE_BANK_FAIL";
export function updateStripeBank(bankId, data) {
  return {
    types: [UPDATE_STRIPE_BANK, UPDATE_STRIPE_BANK_SUCCESS, UPDATE_STRIPE_BANK_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "put",
        url: `/v1/customers/:customerId/sources/${bankId}`,
        data
      }
    }
  };
}
