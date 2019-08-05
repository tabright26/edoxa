import { combineReducers } from 'redux';
import { reducer as oidcReducer } from 'redux-oidc';
import { connectRouter } from 'connected-react-router';
import { reducer as arenaReducer } from './arenaReducer';
import { reducer as cashierReducer } from './cashierReducer';
import { reducer as userGamesReducer } from './userGamesReducer';
import { reducer as userReducer } from './userReducer';

export default history =>
  combineReducers({
    router: connectRouter(history),
    oidc: oidcReducer,
    arena: arenaReducer,
    cashier: cashierReducer,
    gameProviders: userGamesReducer,
    user: userReducer
  });
