import { combineReducers } from 'redux';
import { reducer as oidcReducer } from 'redux-oidc';
import { connectRouter } from 'connected-react-router';
import { reducer as arenaReducer } from './reducers/arenaReducer';
import { reducer as cashierReducer } from './reducers/cashierReducer';
import { reducer as userGamesReducer } from './reducers/userGamesReducer';
import { reducer as userReducer } from './reducers/userReducer';

export default history =>
  combineReducers({
    router: connectRouter(history),
    oidc: oidcReducer,
    arena: arenaReducer,
    cashier: cashierReducer,
    gameProviders: userGamesReducer,
    user: userReducer
  });
