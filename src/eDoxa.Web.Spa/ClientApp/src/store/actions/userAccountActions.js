import {
  loadUserAccountBalanceMoneyAsync,
  loadUserAccountBalanceTokenAsync,
  loadUserAccountTransactionsAsync,
  loadUserStripeCardsAsync,
  loadUserStripeBankAccountsAsync
} from '../../services/userAccountService';

export const LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS =
  'LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS';
export const LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS =
  'LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS';
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS =
  'LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS';
export const LOAD_USER_STRIPE_CARDS_SUCCESS = 'LOAD_USER_STRIPE_CARDS_SUCCESS';
export const HAS_USER_STRIPE_BANK_ACCOUNT_SUCCESS =
  'HAS_USER_STRIPE_BANK_ACCOUNT_SUCCESS';

export function loadUserAccountBalanceMoneySuccess(money) {
  return { type: LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS, money };
}

export function loadUserAccountBalanceTokenSuccess(token) {
  return { type: LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS, token };
}

export function loadUserAccountTransactionsSuccess(transactions) {
  return { type: LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, transactions };
}

export function loadUserStripeCardsSuccess(cards) {
  return { type: LOAD_USER_STRIPE_CARDS_SUCCESS, cards };
}

export function hasUserStripeBankAccountSuccess(hasBankAccount) {
  return { type: HAS_USER_STRIPE_BANK_ACCOUNT_SUCCESS, hasBankAccount };
}

export function loadUserAccountBalanceForMoney() {
  return async function(dispatch, getState) {
    try {
      const response = await loadUserAccountBalanceMoneyAsync(getState);
      dispatch(loadUserAccountBalanceMoneySuccess(response.data));
    } catch (error) {
      console.log(error);
    }
  };
}

export function loadUserAccountBalanceForToken() {
  return async function(dispatch, getState) {
    try {
      const response = await loadUserAccountBalanceTokenAsync(getState);
      dispatch(loadUserAccountBalanceTokenSuccess(response.data));
    } catch (error) {
      console.log(error);
    }
  };
}

export function loadUserAccountTransactions() {
  return async function(dispatch, getState) {
    try {
      const response = await loadUserAccountTransactionsAsync(getState);
      dispatch(loadUserAccountTransactionsSuccess(response.data));
    } catch (error) {
      console.log(error);
    }
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
