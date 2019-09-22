import { LoadBankAccountsActionType } from "actions/stripe/creators";
import { IAxiosAction } from "interfaces/axios";
export const initialState = { data: [] };

export const reducer = (state = initialState, action: IAxiosAction<LoadBankAccountsActionType>) => {
  switch (action.type) {
    case "LOAD_BANK_ACCOUNTS_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case "LOAD_BANK_ACCOUNTS_FAIL":
    default:
      return state;
  }
};
