export const LOAD_CARDS = "LOAD_CARDS";
export const LOAD_CARDS_SUCCESS = "LOAD_CARDS_SUCCESS";
export const LOAD_CARDS_FAIL = "LOAD_CARDS_FAIL";
export function loadCards() {
  return {
    types: [LOAD_CARDS, LOAD_CARDS_SUCCESS, LOAD_CARDS_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "get",
        url: `/v1/customers/:customerId/sources?object=card`
      }
    }
  };
}

export const CREATE_CARD = "CREATE_CARD";
export const CREATE_CARD_SUCCESS = "CREATE_CARD_SUCCESS";
export const CREATE_CARD_FAIL = "CREATE_CARD_FAIL";
export function createCard(token) {
  return {
    types: [CREATE_CARD, CREATE_CARD_SUCCESS, CREATE_CARD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "post",
        url: "/v1/customers/:customerId/sources",
        data: {
          source: token
        }
      }
    }
  };
}

export const UPDATE_CARD = "UPDATE_CARD";
export const UPDATE_CARD_SUCCESS = "UPDATE_CARD_SUCCESS";
export const UPDATE_CARD_FAIL = "UPDATE_CARD_FAIL";
export function updateCard(cardId, data) {
  return {
    types: [UPDATE_CARD, UPDATE_CARD_SUCCESS, UPDATE_CARD_FAIL],
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

export const DELETE_CARD = "DELETE_CARD";
export const DELETE_CARD_SUCCESS = "DELETE_CARD_SUCCESS";
export const DELETE_CARD_FAIL = "DELETE_CARD_FAIL";
export function deleteCard(cardId) {
  return {
    types: [DELETE_CARD, DELETE_CARD_SUCCESS, DELETE_CARD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "delete",
        url: `/v1/customers/:customerId/sources/${cardId}`
      }
    }
  };
}

export const LOAD_BANK_ACCOUNTS = "LOAD_BANK_ACCOUNTS";
export const LOAD_BANK_ACCOUNTS_SUCCESS = "LOAD_BANK_ACCOUNTS_SUCCESS";
export const LOAD_BANK_ACCOUNTS_FAIL = "LOAD_BANK_ACCOUNTS_FAIL";
export function loadBankAccounts() {
  return {
    types: [LOAD_BANK_ACCOUNTS, LOAD_BANK_ACCOUNTS_SUCCESS, LOAD_BANK_ACCOUNTS_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "get",
        url: "/v1/accounts/:connectAccountId/external_accounts?object=bank_account"
      }
    }
  };
}

export const CREATE_BANK_ACCOUNT = "CREATE_BANK_ACCOUNT";
export const CREATE_BANK_ACCOUNT_SUCCESS = "CREATE_BANK_ACCOUNT_SUCCESS";
export const CREATE_BANK_ACCOUNT_FAIL = "CREATE_BANK_ACCOUNT_FAIL";
export function createBankAccount(token) {
  return {
    types: [CREATE_BANK_ACCOUNT, CREATE_BANK_ACCOUNT_SUCCESS, CREATE_BANK_ACCOUNT_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "post",
        url: "/v1/accounts/:connectAccountId/external_accounts",
        data: {
          external_account: token
        }
      }
    }
  };
}

export const UPDATE_BANK_ACCOUNT = "UPDATE_BANK_ACCOUNT";
export const UPDATE_BANK_ACCOUNT_SUCCESS = "UPDATE_BANK_ACCOUNT_SUCCESS";
export const UPDATE_BANK_ACCOUNT_FAIL = "UPDATE_BANK_ACCOUNT_FAIL";
export function updateBankAccount(bankAccountId, data) {
  return {
    types: [UPDATE_BANK_ACCOUNT, UPDATE_BANK_ACCOUNT_SUCCESS, UPDATE_BANK_ACCOUNT_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "put",
        url: `/v1/customers/:customerId/sources/${bankAccountId}`,
        data
      }
    }
  };
}

export const DELETE_BANK_ACCOUNT = "DELETE_BANK_ACCOUNT";
export const DELETE_BANK_ACCOUNT_SUCCESS = "DELETE_BANK_ACCOUNT_SUCCESS";
export const DELETE_BANK_ACCOUNT_FAIL = "DELETE_BANK_ACCOUNT_FAIL";
export function deleteBankAccount(bankAccountId) {
  return {
    types: [DELETE_BANK_ACCOUNT, DELETE_BANK_ACCOUNT_SUCCESS, DELETE_BANK_ACCOUNT_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "delete",
        url: `/v1/customers/:customerId/sources/${bankAccountId}`
      }
    }
  };
}
