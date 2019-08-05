import { combineReducers } from 'redux';
import { reducer as oidcReducer } from 'redux-oidc';
import { reducer as routerReducer } from '../middlewares/routerMiddleware';
import { reducer as arenaReducer } from './arenaReducer';
import { reducer as cashierReducer } from './cashierReducer';
import { reducer as userGamesReducer } from './userGamesReducer';
import { reducer as userReducer } from './userReducer';

export default () =>
  combineReducers({
    router: routerReducer,
    oidc: oidcReducer,
    arena: arenaReducer,
    cashier: cashierReducer,
    gameProviders: userGamesReducer,
    user: userReducer
  });
