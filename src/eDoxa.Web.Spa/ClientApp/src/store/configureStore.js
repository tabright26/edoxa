import { createStore, compose, applyMiddleware } from "redux";
import { loadUser } from "redux-oidc";
import { loadUsers } from "./actions/identityActions";

import { middleware as thunkMiddleware } from "./middlewares/thunkMiddleware";
import { middleware as routerMiddleware } from "./middlewares/routerMiddleware";
import { middleware as axiosMiddleware } from "./middlewares/axiosMiddleware";
import { middleware as signalrMiddleware } from "./middlewares/signalrMiddleware";
import { middleware as loggerMiddleware } from "./middlewares/loggerMiddleware";

import createRootReducer from "./reducers/rootReducer";

import userManager from "../utils/userManager";

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export default initialState => {
  const store = createStore(createRootReducer(), initialState, composeEnhancers(applyMiddleware(thunkMiddleware, axiosMiddleware, signalrMiddleware, routerMiddleware, loggerMiddleware)));
  loadUser(store, userManager);
  store.dispatch(loadUsers());
  return store;
};
