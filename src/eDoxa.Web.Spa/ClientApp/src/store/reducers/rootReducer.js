import { combineReducers } from "redux";
import { reducer as oidcReducer } from "redux-oidc";
import { reducer as routerReducer } from "../middlewares/routerMiddleware";
import { reducer as arenaReducer } from "./arenaReducer";
import { reducer as userReducer } from "./userReducer";
import { reducer as usersReducer } from "./usersReducer";

export default () =>
  combineReducers({
    router: routerReducer,
    oidc: oidcReducer,
    arena: arenaReducer,
    user: userReducer,
    users: usersReducer
  });
