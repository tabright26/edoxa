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
