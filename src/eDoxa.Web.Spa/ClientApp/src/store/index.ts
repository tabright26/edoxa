import { createStore, compose, applyMiddleware } from "redux";

import { RootState } from "store/root/types";

import { reducer as rootReducer } from "store/root/reducer";

import { middleware as thunkMiddleware } from "store/middlewares/thunk/middleware";
import { middleware as routerMiddleware } from "store/middlewares/router/middleware";
import { middleware as axiosMiddleware } from "store/middlewares/axios/middleware";
import { middleware as signalrMiddleware } from "store/middlewares/signalr/middleware";
import { middleware as loggerMiddleware } from "store/middlewares/logger/middleware";

import userManager from "store/middlewares/oidc/userManager";

import { loadUser } from "redux-oidc";
import { loadDoxaTags } from "store/root/doxatags/actions";

// This enables the webpack development tools such as the Hot Module Replacement.
const composeEnhancers = (window["__REDUX_DEVTOOLS_EXTENSION_COMPOSE__"] as typeof compose) || compose;

export const configureStore = (initialState: RootState) => {
  const store = createStore(rootReducer, initialState, composeEnhancers(applyMiddleware(thunkMiddleware, axiosMiddleware, signalrMiddleware, routerMiddleware, loggerMiddleware)));
  loadUser(store, userManager);
  const action: any = loadDoxaTags();
  store.dispatch(action);
  return store;
};
