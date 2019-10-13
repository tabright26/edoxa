import { combineReducers } from "redux";

import { reducer as routerReducer } from "store/middlewares/routerMiddleware";
import { reducer as formReducer } from "redux-form";
import { reducer as modalReducer } from "redux-modal";
import { reducer as oidcReducer } from "redux-oidc";
import { reducer as stripeReducer } from "store/root/stripe/reducer";
import { reducer as arenaReducer } from "store/root/arena/reducer";
import { reducer as userReducer } from "store/root/user/reducer";
import { reducer as organizationsReducer } from "store/root/organizations/reducer";
import { reducer as doxaTagsReducer } from "store/root/doxaTags/reducer";
import { reducer as toastrReducer } from "react-redux-toastr";
import { reducer as localizeReducer } from "utils/localize";

export const reducer = combineReducers({
  router: routerReducer,
  form: formReducer,
  modal: modalReducer,
  toastr: toastrReducer,
  oidc: oidcReducer,
  localize: localizeReducer,
  stripe: stripeReducer,
  arena: arenaReducer,
  user: userReducer,
  doxaTags: doxaTagsReducer,
  organizations: organizationsReducer
});
