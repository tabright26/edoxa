import { combineReducers } from 'redux';
import { reducer as arenaChallengesReducer } from './arenaChallengesReducer';

export const reducer = combineReducers({
  challenges: arenaChallengesReducer
});
