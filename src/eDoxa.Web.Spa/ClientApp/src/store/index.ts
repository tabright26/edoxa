import { createStore, compose, applyMiddleware } from "redux";

import { RootState } from "store/types";

import { reducer as rootReducer } from "store/reducer";
import { epics as rootEpic } from "store/epic";

import { middleware as thunkMiddleware } from "utils/thunk/middleware";
import { middleware as routerMiddleware } from "utils/router/middleware";
import { middleware as axiosMiddleware } from "utils/axios/middleware";
import { middleware as signalrMiddleware } from "utils/signalr/middleware";
import { middleware as loggerMiddleware } from "utils/logger/middleware";
import { middleware as epicMiddleware } from "utils/observable/middleware";

// This enables the webpack development tools such as the Hot Module Replacement.
const composeEnhancers =
  (window["__REDUX_DEVTOOLS_EXTENSION_COMPOSE__"] as typeof compose) || compose;

const configure = (initialState: RootState) => {
  if (process.env.NODE_ENV === "production") {
    return createStore(
      rootReducer,
      initialState,
      applyMiddleware(
        thunkMiddleware,
        axiosMiddleware,
        signalrMiddleware,
        routerMiddleware,
        epicMiddleware
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
          loggerMiddleware,
          epicMiddleware
        )
      )
    );
  }
};

export const configureStore = (initialState: RootState) => {
  const store = configure(initialState);
  epicMiddleware.run(rootEpic);
  return store;
};
