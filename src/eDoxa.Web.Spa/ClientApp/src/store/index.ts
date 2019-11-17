import { createStore, compose, applyMiddleware } from "redux";

import { RootState } from "store/types";

import { reducer as rootReducer } from "store/reducer";

import { middleware as thunkMiddleware } from "utils/thunk/middleware";
import { middleware as routerMiddleware } from "utils/router/middleware";
import { middleware as axiosMiddleware } from "utils/axios/middleware";
import { middleware as signalrMiddleware } from "utils/signalr/middleware";
import { middleware as loggerMiddleware } from "utils/logger/middleware";

import userManager from "utils/oidc/userManager";

import { loadUser } from "redux-oidc";

// This enables the webpack development tools such as the Hot Module Replacement.
const composeEnhancers =
  (window["__REDUX_DEVTOOLS_EXTENSION_COMPOSE__"] as typeof compose) || compose;

const createReduxStore = (initialState: RootState) => {
  if (process.env.NODE_ENV === "production") {
    return createStore(
      rootReducer,
      initialState,
      applyMiddleware(
        thunkMiddleware,
        axiosMiddleware,
        signalrMiddleware,
        routerMiddleware
      )
    );
  } else {
    return createStore(
      rootReducer,
      initialState,
      composeEnhancers(
        applyMiddleware(
          thunkMiddleware,
          axiosMiddleware,
          signalrMiddleware,
          routerMiddleware,
          loggerMiddleware
        )
      )
    );
  }
};

export const configureStore = (initialState: RootState) => {
  const store = createReduxStore(initialState);
  loadUser(store, userManager);
  return store;
};
