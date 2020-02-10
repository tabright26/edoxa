import { createStore, compose, applyMiddleware } from "redux";

import { reducer as rootReducer } from "store/reducer";
import { epic as rootEpic } from "store/epic";

import { middleware as thunkMiddleware } from "utils/thunk/middleware";
import { middleware as routerMiddleware } from "utils/router/middleware";
import { middleware as axiosMiddleware } from "utils/axios/middleware";
import { middleware as signalrMiddleware } from "utils/signalr/middleware";
import { middleware as loggerMiddleware } from "utils/logger/middleware";
import { middleware as epicMiddleware } from "utils/observable/middleware";

// This enables the webpack development tools such as the Hot Module Replacement.
const composeEnhancers =
  (window["__REDUX_DEVTOOLS_EXTENSION_COMPOSE__"] as typeof compose) || compose;

const store = createStore(
  rootReducer,
  composeEnhancers(
    process.env.NODE_ENV === "production" || process.env.NODE_ENV === "test"
      ? applyMiddleware(
          thunkMiddleware,
          axiosMiddleware,
          signalrMiddleware,
          routerMiddleware,
          epicMiddleware
        )
      : applyMiddleware(
          thunkMiddleware,
          axiosMiddleware,
          signalrMiddleware,
          routerMiddleware,
          loggerMiddleware,
          epicMiddleware
        )
  )
);

epicMiddleware.run(rootEpic);

export default store;
