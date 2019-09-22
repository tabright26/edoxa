import { IAxiosAction } from "interfaces/axios";
import { LoadUserAccountTransactionsActionType } from "actions/cashier/actionTypes";

export const initialState = [];

export const reducer = (state = initialState, action: IAxiosAction<LoadUserAccountTransactionsActionType>) => {
  switch (action.type) {
    case "LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return state.filter(oldTransaction => !data.some(newTransaction => newTransaction.id === oldTransaction.id)).concat(data);
      }
    case "LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL":
    default:
      return state;
  }
};
