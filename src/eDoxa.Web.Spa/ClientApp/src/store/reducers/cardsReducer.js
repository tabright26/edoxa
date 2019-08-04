import { LOAD_USER_STRIPE_CARDS_SUCCESS } from '../actions/userAccountActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_USER_STRIPE_CARDS_SUCCESS:
      return action.cards;
    default:
      return state;
  }
};
