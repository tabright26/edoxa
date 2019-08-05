import { createStore, compose, applyMiddleware } from 'redux';
import { persistStore } from 'redux-persist';
import { loadUser } from 'redux-oidc';

import { middleware as thunkMiddleware } from './middlewares/thunkMiddleware';
import { middleware as routerMiddleware } from './middlewares/routerMiddleware';
import { middleware as axiosMiddleware } from './middlewares/axiosMiddleware';
import { middleware as signalrMiddleware } from './middlewares/signalrMiddleware';
import { middleware as loggerMiddleware } from './middlewares/loggerMiddleware';

import userManager from '../utils/userManager';
import createRootReducer from './reducers/rootReducer';

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export default initialState => {
  const store = createStore(
    createRootReducer(),
    initialState,
    composeEnhancers(
      applyMiddleware(
        thunkMiddleware,
        routerMiddleware,
        axiosMiddleware,
        signalrMiddleware,
        loggerMiddleware
      )
    )
  );
  const persistor = persistStore(store);
  loadUser(store, userManager);
  return { store, persistor };
};
