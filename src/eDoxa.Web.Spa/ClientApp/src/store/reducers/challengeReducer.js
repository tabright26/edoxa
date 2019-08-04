import {
  LOAD_CHALLENGES_SUCCESS,
  LOAD_CHALLENGES_FAIL,
  LOAD_CHALLENGE_SUCCESS,
  LOAD_CHALLENGE_FAIL
} from '../actions/arenaChallengeActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_CHALLENGES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return [...state, ...data];
      }
    case LOAD_CHALLENGES_FAIL:
      console.log(action.payload.error);
      return state;
    case LOAD_CHALLENGE_SUCCESS:
      return [...state, action.challenge];
    case LOAD_CHALLENGE_FAIL:
      console.log(action.payload.error);
      return state;
    default:
      return state;
  }
};
