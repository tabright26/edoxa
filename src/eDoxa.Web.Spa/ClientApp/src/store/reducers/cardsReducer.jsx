import { FETCH_CARDS_SUCCESS } from '../actions/cashierActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case FETCH_CARDS_SUCCESS:
      return action.cards;
    default:
      return state;
  }
};
