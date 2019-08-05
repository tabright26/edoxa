export const LOAD_USER_ACCOUNT_BALANCE_MONEY =
  'LOAD_USER_ACCOUNT_BALANCE_MONEY';
export const LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS =
  'LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS';
export const LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL =
  'LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL';
export function loadUserAccountBalanceForMoney() {
  return function(dispatch, getState) {
    dispatch({
      types: [
        LOAD_USER_ACCOUNT_BALANCE_MONEY,
        LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS,
        LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL
      ],
      payload: {
        request: {
          method: 'get',
          url: '/cashier/api/account/balance/money',
          headers: {
            authorization: `Bearer ${getState().oidc.user.access_token}`,
            accept: 'application/json'
          }
        }
      }
    });
  };
}

export const LOAD_USER_ACCOUNT_BALANCE_TOKEN =
  'LOAD_USER_ACCOUNT_BALANCE_TOKEN';
export const LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS =
  'LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS';
export const LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL =
  'LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL';
export function loadUserAccountBalanceForToken() {
  return function(dispatch, getState) {
    dispatch({
      types: [
        LOAD_USER_ACCOUNT_BALANCE_TOKEN,
        LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS,
        LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL
      ],
      payload: {
        request: {
          method: 'get',
          url: '/cashier/api/account/balance/token',
          headers: {
            authorization: `Bearer ${getState().oidc.user.access_token}`,
            accept: 'application/json'
          }
        }
      }
    });
  };
}

export const LOAD_USER_ACCOUNT_TRANSACTIONS = 'LOAD_USER_ACCOUNT_TRANSACTIONS';
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS =
  'LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS';
export const LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL =
  'LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL';
export function loadUserAccountTransactions() {
  return function(dispatch, getState) {
    dispatch({
      types: [
        LOAD_USER_ACCOUNT_TRANSACTIONS,
        LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
        LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL
      ],
      payload: {
        request: {
          method: 'get',
          url: '/cashier/api/transactions',
          headers: {
            authorization: `Bearer ${getState().oidc.user.access_token}`,
            accept: 'application/json'
          }
        }
      }
    });
  };
}

export const LOAD_USER_STRIPE_CARDS = 'LOAD_USER_STRIPE_CARDS';
export const LOAD_USER_STRIPE_CARDS_SUCCESS = 'LOAD_USER_STRIPE_CARDS_SUCCESS';
export const LOAD_USER_STRIPE_CARDS_FAIL = 'LOAD_USER_STRIPE_CARDS_FAIL';
export function loadUserStripeCards() {
  return async function(dispatch, getState) {
    const state = getState();
    const { profile } = state.oidc.user;
    dispatch({
      types: [
        LOAD_USER_STRIPE_CARDS,
        LOAD_USER_STRIPE_CARDS_SUCCESS,
        LOAD_USER_STRIPE_CARDS_FAIL
      ],
      payload: {
        client: 'stripe',
        request: {
          method: 'get',
          url: `/v1/customers/${
            profile['stripe:customerId']
          }/sources?object=card`,
          headers: {
            authorization: `Bearer ${process.env.REACT_APP_STRIPE_APIKEY}`,
            accept: 'application/json'
          }
        }
      }
    });
  };
}

export const LOAD_USER_STRIPE_BANK_ACCOUNTS = 'LOAD_USER_STRIPE_BANK_ACCOUNTS';
export const LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS =
  'LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS';
export const LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL =
  'LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL';
export function loadUserStripeBankAccounts() {
  return async function(dispatch, getState) {
    const state = getState();
    const { profile } = state.oidc.user;
    dispatch({
      types: [
        LOAD_USER_STRIPE_BANK_ACCOUNTS,
        LOAD_USER_STRIPE_BANK_ACCOUNTS_SUCCESS,
        LOAD_USER_STRIPE_BANK_ACCOUNTS_FAIL
      ],
      payload: {
        client: 'stripe',
        request: {
          method: 'get',
          url: `/v1/customers/${
            profile['stripe:customerId']
          }/sources?object=bank_account`,
          headers: {
            authorization: `Bearer ${process.env.REACT_APP_STRIPE_APIKEY}`,
            accept: 'application/json'
          }
        }
      }
    });
  };
}
