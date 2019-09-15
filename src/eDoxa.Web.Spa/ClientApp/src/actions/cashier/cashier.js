export const LOAD_USER_ACCOUNT_BALANCE_MONEY = "LOAD_USER_ACCOUNT_BALANCE_MONEY";
export const LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS = "LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS";
export const LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL = "LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL";
export function loadUserAccountBalanceForMoney() {
  return {
    types: [LOAD_USER_ACCOUNT_BALANCE_MONEY, LOAD_USER_ACCOUNT_BALANCE_MONEY_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_MONEY_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/cashier/api/account/balance/money"
      }
    }
  };
}

export const LOAD_USER_ACCOUNT_BALANCE_TOKEN = "LOAD_USER_ACCOUNT_BALANCE_TOKEN";
export const LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS = "LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS";
export const LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL = "LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL";
export function loadUserAccountBalanceForToken() {
  return {
    types: [LOAD_USER_ACCOUNT_BALANCE_TOKEN, LOAD_USER_ACCOUNT_BALANCE_TOKEN_SUCCESS, LOAD_USER_ACCOUNT_BALANCE_TOKEN_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/cashier/api/account/balance/token"
      }
    }
  };
}

export const LOAD_USER_ACCOUNT_TRANSACTIONS = "LOAD_USER_ACCOUNT_TRANSACTIONS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS = "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS";
export const LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL = "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL";
export function loadUserAccountTransactions() {
  return {
    types: [LOAD_USER_ACCOUNT_TRANSACTIONS, LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL],
    payload: {
      request: {
        method: "get",
        url: "/cashier/api/transactions"
      }
    }
  };
}
