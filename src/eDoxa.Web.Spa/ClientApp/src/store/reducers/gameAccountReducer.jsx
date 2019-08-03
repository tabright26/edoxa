import { FETCH_LEAGUEOFLEGENDACCOUNT_SUCCESS } from '../actions/gameAccountActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case FETCH_LEAGUEOFLEGENDACCOUNT_SUCCESS:
      return action.leagueOfLegendAccount;
    default:
      return state;
  }
};
