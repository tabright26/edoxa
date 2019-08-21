import { combineReducers } from "redux";
import { reducer as oidcReducer } from "redux-oidc";
import { reducer as formReducer } from "redux-form";
import { reducer as modalReducer } from "redux-modal";
import { reducer as routerReducer } from "../middlewares/routerMiddleware";
import { reducer as arenaReducer } from "./arenaReducer";
import { reducer as userReducer } from "./userReducer";
import { reducer as doxaTagsReducer } from "./doxaTagsReducer";

export default () =>
  combineReducers({
    router: routerReducer,
    form: formReducer,
    modal: modalReducer,
    oidc: oidcReducer,
    arena: arenaReducer,
    user: userReducer,
    doxaTags: doxaTagsReducer
  });
