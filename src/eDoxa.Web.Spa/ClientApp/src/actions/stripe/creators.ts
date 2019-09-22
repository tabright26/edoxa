import { IAxiosActionCreator } from "interfaces/axios";

export type LoadPaymentMethodsActionType = "LOAD_PAYMENTMETHODS" | "LOAD_PAYMENTMETHODS_SUCCESS" | "LOAD_PAYMENTMETHODS_FAIL";
export function loadPaymentMethods(customer: string, type: "card"): IAxiosActionCreator<LoadPaymentMethodsActionType> {
  return {
    types: ["LOAD_PAYMENTMETHODS", "LOAD_PAYMENTMETHODS_SUCCESS", "LOAD_PAYMENTMETHODS_FAIL"],
    payload: {
      client: "stripe",
      request: {
        method: "GET",
        url: `/v1/payment_methods?customer=${customer}&type=${type}`
      }
    }
  };
}

export type AttachPaymentMethodActionType = "ATTACH_PAYMENTMETHOD" | "ATTACH_PAYMENTMETHOD_SUCCESS" | "ATTACH_PAYMENTMETHOD_FAIL";
export function attachPaymentMethod(paymentMethodId: string, customer: string): IAxiosActionCreator<AttachPaymentMethodActionType> {
  return {
    types: ["ATTACH_PAYMENTMETHOD", "ATTACH_PAYMENTMETHOD_SUCCESS", "ATTACH_PAYMENTMETHOD_FAIL"],
    payload: {
      client: "stripe",
      request: {
        method: "POST",
        url: `/v1/payment_methods/${paymentMethodId}/attach`,
        data: {
          customer
        }
      }
    }
  };
}

export type DetachPaymentMethodActionType = "DETACH_PAYMENTMETHOD" | "DETACH_PAYMENTMETHOD_SUCCESS" | "DETACH_PAYMENTMETHOD_FAIL";
export function detachPaymentMethod(paymentMethodId: string): IAxiosActionCreator<DetachPaymentMethodActionType> {
  return {
    types: ["DETACH_PAYMENTMETHOD", "DETACH_PAYMENTMETHOD_SUCCESS", "DETACH_PAYMENTMETHOD_FAIL"],
    payload: {
      client: "stripe",
      request: {
        method: "POST",
        url: `/v1/payment_methods/${paymentMethodId}/detach`
      }
    }
  };
}

export type UpdatePaymentMethodActionType = "UPDATE_PAYMENTMETHOD" | "UPDATE_PAYMENTMETHOD_SUCCESS" | "UPDATE_PAYMENTMETHOD_FAIL";
export function updatePaymentMethod(paymentMethodId: string, exp_month: number, exp_year: number): IAxiosActionCreator<UpdatePaymentMethodActionType> {
  return {
    types: ["UPDATE_PAYMENTMETHOD", "UPDATE_PAYMENTMETHOD_SUCCESS", "UPDATE_PAYMENTMETHOD_FAIL"],
    payload: {
      client: "stripe",
      request: {
        method: "POST",
        url: `/v1/payment_methods/${paymentMethodId}?card[exp_month]=${exp_month}&card[exp_year]=${exp_year}`
      }
    }
  };
}

export type LoadBankAccountsActionType = "LOAD_BANK_ACCOUNTS" | "LOAD_BANK_ACCOUNTS_SUCCESS" | "LOAD_BANK_ACCOUNTS_FAIL";
export function loadBankAccounts(): IAxiosActionCreator<LoadBankAccountsActionType> {
  return {
    types: ["LOAD_BANK_ACCOUNTS", "LOAD_BANK_ACCOUNTS_SUCCESS", "LOAD_BANK_ACCOUNTS_FAIL"],
    payload: {
      client: "stripe",
      request: {
        method: "GET",
        url: "/v1/accounts/:connectAccountId/external_accounts?object=bank_account"
      }
    }
  };
}

export type CreateBankAccountActionType = "CREATE_BANK_ACCOUNT" | "CREATE_BANK_ACCOUNT_SUCCESS" | "CREATE_BANK_ACCOUNT_FAIL";
export function createBankAccount(token: string): IAxiosActionCreator<CreateBankAccountActionType> {
  return {
    types: ["CREATE_BANK_ACCOUNT", "CREATE_BANK_ACCOUNT_SUCCESS", "CREATE_BANK_ACCOUNT_FAIL"],
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

export type UpdateBankAccountActionType = "UPDATE_BANK_ACCOUNT" | "UPDATE_BANK_ACCOUNT_SUCCESS" | "UPDATE_BANK_ACCOUNT_FAIL";
export function updateBankAccount(bankAccountId: string, data: any): IAxiosActionCreator<UpdateBankAccountActionType> {
  return {
    types: ["UPDATE_BANK_ACCOUNT", "UPDATE_BANK_ACCOUNT_SUCCESS", "UPDATE_BANK_ACCOUNT_FAIL"],
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

export type DeleteBankAccountActionType = "DELETE_BANK_ACCOUNT" | "DELETE_BANK_ACCOUNT_SUCCESS" | "DELETE_BANK_ACCOUNT_FAIL";
export function deleteBankAccount(bankAccountId: string): IAxiosActionCreator<DeleteBankAccountActionType> {
  return {
    types: ["DELETE_BANK_ACCOUNT", "DELETE_BANK_ACCOUNT_SUCCESS", "DELETE_BANK_ACCOUNT_FAIL"],
    payload: {
      client: "stripe",
      request: {
        method: "DELETE",
        url: `/v1/customers/:customerId/sources/${bankAccountId}`
      }
    }
  };
}
