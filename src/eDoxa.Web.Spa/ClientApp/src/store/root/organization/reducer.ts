import { combineReducers } from "redux";

import { reducer as clanReducer } from "./clan/reducer";
import { reducer as logoReducer } from "./logo/reducer";
import { reducer as memberReducer } from "./member/reducer";
import { reducer as candidatureReducer } from "./candidature/reducer";
import { reducer as invitationReducer } from "./invitation/reducer";

export const reducer = combineReducers({
  clan: clanReducer,
  logo: logoReducer,
  member: memberReducer,
  candidature: candidatureReducer,
  invitation: invitationReducer
});
