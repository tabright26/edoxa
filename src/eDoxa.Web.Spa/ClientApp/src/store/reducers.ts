import { combineReducers } from "redux";

import { reducer as routerReducer } from "middlewares/routerMiddleware";
import { reducer as formReducer } from "redux-form";
import { reducer as modalReducer } from "redux-modal";
import { reducer as oidcReducer } from "redux-oidc";
import { reducer as stripeReducer } from "store/stripe/reducer";
import { reducer as arenaReducer } from "store/arena/reducer";
import { reducer as userReducer } from "store/user/reducer";
import { reducer as doxaTagsReducer } from "store/doxaTags/reducer";
import { reducer as toastrReducer } from "react-redux-toastr";

const rootReducer = combineReducers({
  router: routerReducer,
  form: formReducer,
  modal: modalReducer,
  toastr: toastrReducer,
  oidc: oidcReducer,
  stripe: stripeReducer,
  arena: arenaReducer,
  user: userReducer,
  doxaTags: doxaTagsReducer
});

export default rootReducer;
