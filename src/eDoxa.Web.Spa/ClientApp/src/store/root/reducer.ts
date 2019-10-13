import { combineReducers } from "redux";

import { reducer as routerReducer } from "store/middlewares/router/reducer";
import { reducer as formReducer } from "store/middlewares/form/reducer";
import { reducer as modalReducer } from "store/middlewares/modal/reducer";
import { reducer as toastrReducer } from "store/middlewares/toastr/reducer";
import { reducer as oidcReducer } from "store/middlewares/oidc/reducer";
import { reducer as localizeReducer } from "store/middlewares/localize/reducer";
import { reducer as paymentReducer } from "store/root/payment/reducer";
import { reducer as arenaReducer } from "store/root/arena/reducer";
import { reducer as userReducer } from "store/root/user/reducer";
import { reducer as organizationsReducer } from "store/root/organizations/reducer";
import { reducer as doxaTagsReducer } from "store/root/doxaTags/reducer";

export const reducer = combineReducers({
  router: routerReducer,
  form: formReducer,
  modal: modalReducer,
  toastr: toastrReducer,
  oidc: oidcReducer,
  localize: localizeReducer,
  payment: paymentReducer,
  arena: arenaReducer,
  user: userReducer,
  doxaTags: doxaTagsReducer,
  organizations: organizationsReducer
});
