import { createStore, compose, applyMiddleware } from "redux";
import { loadUser } from "redux-oidc";
import { loadDoxaTags } from "store/root/doxaTags/actions";
import { middleware as thunkMiddleware } from "store/middlewares/thunkMiddleware";
import { middleware as routerMiddleware } from "store/middlewares/routerMiddleware";
import { middleware as axiosMiddleware } from "store/middlewares/axiosMiddleware";
import { middleware as signalrMiddleware } from "store/middlewares/signalrMiddleware";
import { middleware as loggerMiddleware } from "store/middlewares/loggerMiddleware";
import userManager from "utils/userManager";
import { reducer as rootReducer } from "store/root/reducer";
import { RootState } from "store/root/types";

// This enables the webpack development tools such as the Hot Module Replacement.
const composeEnhancers = (window["__REDUX_DEVTOOLS_EXTENSION_COMPOSE__"] as typeof compose) || compose;

export const configureStore = (initialState: RootState) => {
  const store = createStore(rootReducer, initialState, composeEnhancers(applyMiddleware(thunkMiddleware, axiosMiddleware, signalrMiddleware, routerMiddleware, loggerMiddleware)));
  loadUser(store, userManager);
  const action: any = loadDoxaTags();
  store.dispatch(action);
  return store;
};
