import {
  fetchTransactionsAsync,
  findAccountBalanceAsync,
  fetchCardsAsync,
  hasBankAccountAsync
} from '../../services/userAccountService';

export const LOAD_USER_ACCOUNT_BALANCE_SUCCESS =
  'LOAD_USER_ACCOUNT_BALANCE_SUCCESS';
export const LOAD_USER_TRANSACTIONS_SUCCESS = 'LOAD_USER_TRANSACTIONS_SUCCESS';
export const FETCH_CARDS_SUCCESS = 'FETCH_CARDS_SUCCESS';
export const HAS_BANKACCOUNT_SUCCESS = 'HAS_BANKACCOUNT_SUCCESS';

export function loadUserAccountBalanceSuccess(balance) {
  return { type: LOAD_USER_ACCOUNT_BALANCE_SUCCESS, balance };
}

export function loadUserTransactionsSuccess(transactions) {
  return { type: LOAD_USER_TRANSACTIONS_SUCCESS, transactions };
}

export function fetchCardsSuccess(cards) {
  return { type: FETCH_CARDS_SUCCESS, cards };
}

export function hasBankAccountSuccess(hasBankAccount) {
  return { type: HAS_BANKACCOUNT_SUCCESS, hasBankAccount };
}

export function findAccountBalance(currency) {
  return async function(dispatch, getState) {
    try {
      const response = await findAccountBalanceAsync(currency, getState);
      dispatch(loadUserAccountBalanceSuccess(response.data));
    } catch (error) {
      console.log(error);
    }
  };
}

export function fetchTransactions() {
  return async function(dispatch, getState) {
    try {
      const response = await fetchTransactionsAsync(getState);
      dispatch(loadUserTransactionsSuccess(response.data));
    } catch (error) {
      console.log(error);
    }
  };
}

export function fetchCards() {
  return async function(dispatch, getState) {
    try {
      const response = await fetchCardsAsync(getState);
      dispatch(fetchCardsSuccess(response.data.data));
    } catch (error) {
      console.log(error);
    }
  };
}

export function hasBankAccount() {
  return async function(dispatch, getState) {
    try {
      const response = await hasBankAccountAsync(getState);
      dispatch(hasBankAccountSuccess(response.data.data.length >= 1));
    } catch (error) {
      console.log(error);
    }
  };
}
