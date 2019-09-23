import {
  LOAD_BANK_ACCOUNTS,
  LOAD_BANK_ACCOUNTS_SUCCESS,
  LOAD_BANK_ACCOUNTS_FAIL,
  CREATE_BANK_ACCOUNT,
  CREATE_BANK_ACCOUNT_SUCCESS,
  CREATE_BANK_ACCOUNT_FAIL,
  UPDATE_BANK_ACCOUNT,
  UPDATE_BANK_ACCOUNT_SUCCESS,
  UPDATE_BANK_ACCOUNT_FAIL,
  DELETE_BANK_ACCOUNT,
  DELETE_BANK_ACCOUNT_SUCCESS,
  DELETE_BANK_ACCOUNT_FAIL,
  BankAccountsActionCreators
} from "./types";

export function loadBankAccounts(): BankAccountsActionCreators {
  return {
    types: [LOAD_BANK_ACCOUNTS, LOAD_BANK_ACCOUNTS_SUCCESS, LOAD_BANK_ACCOUNTS_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "GET",
        url: "/v1/accounts/:connectAccountId/external_accounts?object=bank_account"
      }
    }
  };
}

export function createBankAccount(token: string): BankAccountsActionCreators {
  return {
    types: [CREATE_BANK_ACCOUNT, CREATE_BANK_ACCOUNT_SUCCESS, CREATE_BANK_ACCOUNT_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "POST",
        url: "/v1/accounts/:connectAccountId/external_accounts",
        data: {
          external_account: token
        }
      }
    }
  };
}

export function updateBankAccount(bankAccountId: string, data: any): BankAccountsActionCreators {
  return {
    types: [UPDATE_BANK_ACCOUNT, UPDATE_BANK_ACCOUNT_SUCCESS, UPDATE_BANK_ACCOUNT_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "PUT",
        url: `/v1/customers/:customerId/sources/${bankAccountId}`,
        data
      }
    }
  };
}

export function deleteBankAccount(bankAccountId: string): BankAccountsActionCreators {
  return {
    types: [DELETE_BANK_ACCOUNT, DELETE_BANK_ACCOUNT_SUCCESS, DELETE_BANK_ACCOUNT_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "DELETE",
        url: `/v1/customers/:customerId/sources/${bankAccountId}`
      }
    }
  };
}
