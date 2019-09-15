import { combineReducers } from "redux";

import { reducer as router } from "../middlewares/routerMiddleware";
import { reducer as form } from "redux-form";
import { reducer as modal } from "redux-modal";
import { reducer as oidc } from "redux-oidc";
import { reducer as stripe } from "../reducers/stripe/reducer";
import { reducer as arena } from "../reducers/arena/reducer";
import { reducer as user } from "../reducers/user/reducer";
import { reducer as doxaTags } from "../reducers/doxaTags/reducer";

const rootReducer = () =>
  combineReducers({
    router,
    form,
    modal,
    oidc,
    stripe,
    arena,
    user,
    doxaTags
  });

export default rootReducer;
