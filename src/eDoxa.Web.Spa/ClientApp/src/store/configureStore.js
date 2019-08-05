import { createStore, compose, applyMiddleware } from 'redux';
import { loadUser } from 'redux-oidc';
import { persistStore } from 'redux-persist';

import createRootReducer from './reducers/rootReducer';

import { middleware as thunkMiddleware } from './middlewares/thunkMiddleware';
import {
  middleware as routerMiddleware,
  history
} from './middlewares/routerMiddleware';
import { middleware as axiosMiddleware } from './middlewares/axiosMiddleware';
import { middleware as signalrMiddleware } from './middlewares/signalrMiddleware';
import { middleware as loggerMiddleware } from './middlewares/loggerMiddleware';

import userManager from '../utils/userManager';

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const initialState = {};

export default () => {
  const store = createStore(
    createRootReducer(history),
    initialState,
    composeEnhancers(
      applyMiddleware(
        thunkMiddleware(),
        routerMiddleware(),
        axiosMiddleware(),
        signalrMiddleware(),
        loggerMiddleware()
      )
    )
  );
  const persistor = persistStore(store);
  loadUser(store, userManager);
  return { store, persistor };
};
