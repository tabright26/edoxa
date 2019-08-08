import {
  LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL
} from '../actions/cashierActions';

export const reducer = (state = [], action) => {
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
      console.log(action.payload);
      return state;
    default:
      return state;
  }
};
