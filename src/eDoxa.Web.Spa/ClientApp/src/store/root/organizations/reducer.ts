import { combineReducers } from "redux";

import { reducer as clansReducer } from "./clans/reducer";
import { reducer as logosReducer } from "./logos/reducer";
import { reducer as membersReducer } from "./members/reducer";
import { reducer as candidaturesReducer } from "./candidatures/reducer";
import { reducer as invitationsReducer } from "./invitations/reducer";

export const reducer = combineReducers({
  clans: clansReducer,
  logos: logosReducer,
  members: membersReducer,
  candidatures: candidaturesReducer,
  invitations: invitationsReducer
});
