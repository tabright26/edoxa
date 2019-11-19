import { combineReducers } from "redux";

import { reducer as routerReducer } from "utils/router/reducer";
import { reducer as formReducer } from "utils/form/reducer";
import { reducer as modalReducer } from "utils/modal/reducer";
import { reducer as toastrReducer } from "utils/toastr/reducer";
import { reducer as localizeReducer } from "utils/localize/reducer";
import { reducer as rootReducer } from "store/root/reducer";

export const reducer = combineReducers({
  router: routerReducer,
  form: formReducer,
  modal: modalReducer,
  toastr: toastrReducer,
  localize: localizeReducer,
  root: rootReducer
});
