import { combineReducers } from "redux";

import { reducer as routerReducer } from "utils/router/reducer";
import { reducer as formReducer } from "utils/form/reducer";
import { reducer as modalReducer } from "utils/modal/reducer";
import { reducer as toastrReducer } from "utils/toastr/reducer";
import { reducer as localizeReducer } from "utils/localize/reducer";
import { reducer as oidcReducer } from "utils/oidc/reducer";
import { reducer as rootReducer } from "store/root/reducer";
import { reducer as staticReducer } from "store/static/reducer";

export const reducer = combineReducers({
  router: routerReducer,
  form: formReducer,
  oidc: oidcReducer,
  modal: modalReducer,
  toastr: toastrReducer,
  localize: localizeReducer,
  root: rootReducer,
  static: staticReducer
});
