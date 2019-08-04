import axios from 'axios';
import { routerMiddleware } from 'connected-react-router';
import { createStore, compose, applyMiddleware } from 'redux';
import { createLogger } from 'redux-logger';
import thunk from 'redux-thunk';
import { multiClientMiddleware } from 'redux-axios-middleware';

import { persistStore } from 'redux-persist';

import createRootReducer from './rootReducer';

import { createBrowserHistory } from 'history';
import { loadUser } from 'redux-oidc';
import userManager from '../utils/userManager';

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const initialState = {};

export const history = createBrowserHistory();

export default () => {
  const store = createStore(
    createRootReducer(history),
    initialState,
    composeEnhancers(
      applyMiddleware(
        thunk,
        routerMiddleware(history),
        multiClientMiddleware({
          default: {
            client: axios.create({
              baseURL: process.env.REACT_APP_WEB_GATEWAY,
              responseType: 'json'
            })
          },
          stripe: {
            client: axios.create({
              baseURL: 'https://api.stripe.com/v1',
              responseType: 'json'
            })
          }
        }),
        createLogger()
      )
    )
  );
  const persistor = persistStore(store);
  loadUser(store, userManager);
  return { store, persistor };
};
