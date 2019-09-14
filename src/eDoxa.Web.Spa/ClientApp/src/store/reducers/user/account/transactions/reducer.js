import { LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS, LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL } from "../../../../actions/cashierActions";

export const initialState = [];

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL:
    default:
      return state;
  }
};
