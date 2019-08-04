import {
  LOAD_CHALLENGES_SUCCESS,
  LOAD_CHALLENGES_FAIL,
  FIND_CHALLENGE_SUCCESS
} from '../actions/arenaChallengeActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_CHALLENGES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_CHALLENGES_FAIL:
      console.log(action.payload.error);
      return state;
    case FIND_CHALLENGE_SUCCESS:
      return [...state, action.challenge];
    default:
      return state;
  }
};
