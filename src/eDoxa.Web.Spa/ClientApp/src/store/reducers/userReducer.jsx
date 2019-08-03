import { combineReducers } from 'redux';
import { reducer as gameAccountReducer } from './gameAccountReducer';

export const reducer = combineReducers({
  gameAccounts: gameAccountReducer
});
