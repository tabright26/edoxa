import { combineReducers } from "redux";

import { reducer as routerReducer } from "../middlewares/routerMiddleware";
import { reducer as formReducer } from "redux-form";
import { reducer as modalReducer } from "redux-modal";
import { reducer as oidcReducer } from "redux-oidc";
import { reducer as stripeReducer } from "./stripe/reducer";
import { reducer as arenaReducer } from "./arena/reducer";
import { reducer as userReducer } from "./user/reducer";
import { reducer as doxaTagsReducer } from "./doxaTags/reducer";

export default () =>
  combineReducers({
    router: routerReducer,
    form: formReducer,
    modal: modalReducer,
    oidc: oidcReducer,
    stripe: stripeReducer,
    arena: arenaReducer,
    user: userReducer,
    doxaTags: doxaTagsReducer
  });
