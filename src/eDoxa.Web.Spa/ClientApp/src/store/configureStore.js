import { createStore, compose, applyMiddleware } from "redux";
import { loadUser } from "redux-oidc";
import { loadDoxaTags } from "../actions/identity/creators";

import { middleware as thunkMiddleware } from "../middlewares/thunkMiddleware";
import { middleware as routerMiddleware } from "../middlewares/routerMiddleware";
import { middleware as axiosMiddleware } from "../middlewares/axiosMiddleware";
import { middleware as signalrMiddleware } from "../middlewares/signalrMiddleware";
import { middleware as loggerMiddleware } from "../middlewares/loggerMiddleware";

import createRootReducer from "./createRootReducer";

import userManager from "../utils/userManager";

// This enables the webpack development tools such as the Hot Module Replacement.
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export default initialState => {
  const store = createStore(createRootReducer(), initialState, composeEnhancers(applyMiddleware(thunkMiddleware, axiosMiddleware, signalrMiddleware, routerMiddleware, loggerMiddleware)));
  loadUser(store, userManager);
  store.dispatch(loadDoxaTags());
  return store;
};
