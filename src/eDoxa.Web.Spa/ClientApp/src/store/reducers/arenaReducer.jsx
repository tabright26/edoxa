import { combineReducers } from 'redux';
import { reducer as challengeReducer } from './challengeReducer';

export const reducer = combineReducers({
  challenges: challengeReducer
});
