import {
  LOAD_USER_GAMES_SUCCESS,
  LOAD_USER_GAMES_FAIL
} from '../actions/userGameActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_USER_GAMES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_USER_GAMES_FAIL:
      console.log(action.payload.error);
      return state;
    default:
      return state;
  }
};
