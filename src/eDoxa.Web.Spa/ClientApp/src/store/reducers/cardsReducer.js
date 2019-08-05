import {
  LOAD_USER_STRIPE_CARDS_SUCCESS,
  LOAD_USER_STRIPE_CARDS_FAIL
} from '../actions/userAccountActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_USER_STRIPE_CARDS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data.data;
      }
    case LOAD_USER_STRIPE_CARDS_FAIL:
      console.log(action.payload.error);
      return state;
    default:
      return state;
  }
};
