import {
  loadUserStripeCardsAsync,
  loadUserStripeBankAccountsAsync
} from '../../services/userAccountService';

export const LOAD_USER_ACCOUNT_BALANCE_MONEY =
  'LOAD_USER_ACCOUNT_BALANCE_MONEY';
export const LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS =
  'LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS';
export const LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL =
  'LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL';

export const LOAD_USER_ACCOUNT_BALANCE_TOKEN =
  'LOAD_USER_ACCOUNT_BALANCE_TOKEN';
export const LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS =
  'LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS';
export const LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL =
  'LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL';

export const LOAD_USER_ACCOUNT_TRANSACTIONS = 'LOAD_USER_ACCOUNT_TRANSACTIONS';
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS =
  'LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS';
export const LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL =
  'LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL';

export const LOAD_USER_STRIPE_CARDS_SUCCESS = 'LOAD_USER_STRIPE_CARDS_SUCCESS';
export const HAS_USER_STRIPE_BANK_ACCOUNT_SUCCESS =
  'HAS_USER_STRIPE_BANK_ACCOUNT_SUCCESS';

export function loadUserStripeCardsSuccess(cards) {
  return { type: LOAD_USER_STRIPE_CARDS_SUCCESS, cards };
}

export function hasUserStripeBankAccountSuccess(hasBankAccount) {
  return { type: HAS_USER_STRIPE_BANK_ACCOUNT_SUCCESS, hasBankAccount };
}

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

export function loadUserStripeCards() {
  return async function(dispatch, getState) {
    try {
      const response = await loadUserStripeCardsAsync(getState);
      dispatch(loadUserStripeCardsSuccess(response.data.data));
    } catch (error) {
      console.log(error);
    }
  };
}

export function hasUserStripeBankAccount() {
  return async function(dispatch, getState) {
    try {
      const response = await loadUserStripeBankAccountsAsync(getState);
      dispatch(hasUserStripeBankAccountSuccess(response.data.data.length >= 1));
    } catch (error) {
      console.log(error);
    }
  };
}
