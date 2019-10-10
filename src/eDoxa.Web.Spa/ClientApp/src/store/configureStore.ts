import { createStore, compose, applyMiddleware } from "redux";
import { loadUser } from "redux-oidc";
import { loadDoxaTags } from "store/doxaTags/actions";
import { middleware as thunkMiddleware } from "middlewares/thunkMiddleware";
import { middleware as routerMiddleware } from "middlewares/routerMiddleware";
import { middleware as axiosMiddleware } from "middlewares/axiosMiddleware";
import { middleware as signalrMiddleware } from "middlewares/signalrMiddleware";
import { middleware as loggerMiddleware } from "middlewares/loggerMiddleware";
import userManager from "utils/userManager";
import rootReducer from "./reducers";
import { RootState } from "./types";

// This enables the webpack development tools such as the Hot Module Replacement.
const composeEnhancers = (window["__REDUX_DEVTOOLS_EXTENSION_COMPOSE__"] as typeof compose) || compose;

export default (initialState: RootState) => {
  const store = createStore(rootReducer, initialState, composeEnhancers(applyMiddleware(thunkMiddleware, axiosMiddleware, signalrMiddleware, routerMiddleware, loggerMiddleware)));
  loadUser(store, userManager);
  const action: any = loadDoxaTags();
  store.dispatch(action);
  return store;
};
