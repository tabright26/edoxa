import {
  FETCH_CHALLENGES_SUCCESS,
  FIND_CHALLENGE_SUCCESS
} from '../actions/arenaChallengesActions';

/*
The state of this reducer correspond to the global state property 'api.challenges'.
store.getState() = {
  router: {}
  form: {}
  oidc: {}
  api: {
    challenges: [] <=
  }
}
*/

export const reducer = (state = [], action) => {
  switch (action.type) {
    case FETCH_CHALLENGES_SUCCESS:
      return action.challenges;
    case FIND_CHALLENGE_SUCCESS:
      return [...state, action.challenge];
    default:
      return state;
  }
};
