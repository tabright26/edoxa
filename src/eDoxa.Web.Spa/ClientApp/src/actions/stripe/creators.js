export const LOAD_PAYMENTMETHODS = "LOAD_PAYMENTMETHODS";
export const LOAD_PAYMENTMETHODS_SUCCESS = "LOAD_PAYMENTMETHODS_SUCCESS";
export const LOAD_PAYMENTMETHODS_FAIL = "LOAD_PAYMENTMETHODS_FAIL";
export function loadPaymentMethods(customer, type) {
  return {
    types: [LOAD_PAYMENTMETHODS, LOAD_PAYMENTMETHODS_SUCCESS, LOAD_PAYMENTMETHODS_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "get",
        url: `/v1/payment_methods?customer=${customer}&type=${type}`
      }
    }
  };
}

export const ATTACH_PAYMENTMETHOD = "ATTACH_PAYMENTMETHOD";
export const ATTACH_PAYMENTMETHOD_SUCCESS = "ATTACH_PAYMENTMETHOD_SUCCESS";
export const ATTACH_PAYMENTMETHOD_FAIL = "ATTACH_PAYMENTMETHOD_FAIL";
export function attachPaymentMethod(paymentMethodId, customer) {
  return {
    types: [ATTACH_PAYMENTMETHOD, ATTACH_PAYMENTMETHOD_SUCCESS, ATTACH_PAYMENTMETHOD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "post",
        url: `/v1/payment_methods/${paymentMethodId}/attach`,
        data: {
          customer
        }
      }
    }
  };
}

export const DETACH_PAYMENTMETHOD = "DETACH_PAYMENTMETHOD";
export const DETACH_PAYMENTMETHOD_SUCCESS = "DETACH_PAYMENTMETHOD_SUCCESS";
export const DETACH_PAYMENTMETHOD_FAIL = "DETACH_PAYMENTMETHOD_FAIL";
export function detachPaymentMethod(paymentMethodId) {
  return {
    types: [DETACH_PAYMENTMETHOD, DETACH_PAYMENTMETHOD_SUCCESS, DETACH_PAYMENTMETHOD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "post",
        url: `/v1/payment_methods/${paymentMethodId}/detach`
      }
    }
  };
}

export const UPDATE_PAYMENTMETHOD = "UPDATE_PAYMENTMETHOD";
export const UPDATE_PAYMENTMETHOD_SUCCESS = "UPDATE_PAYMENTMETHOD_SUCCESS";
export const UPDATE_PAYMENTMETHOD_FAIL = "UPDATE_PAYMENTMETHOD_FAIL";
export function updatePaymentMethod(paymentMethodId, exp_month, exp_year) {
  return {
    types: [UPDATE_PAYMENTMETHOD, UPDATE_PAYMENTMETHOD_SUCCESS, UPDATE_PAYMENTMETHOD_FAIL],
    payload: {
      client: "stripe",
      request: {
        method: "post",
        url: `/v1/payment_methods/${paymentMethodId}?card[exp_month]=${exp_month}&card[exp_year]=${exp_year}`
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
