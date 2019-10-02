import { combineReducers } from "redux";

import { reducer as clansReducer } from "./clans/reducer";
import { reducer as clanReducer } from "./clan/reducer";
import { reducer as membersReducer } from "./clan/reducer";
import { reducer as candidaturesReducer } from "./candidatures/reducer";
import { reducer as invitationsReducer } from "./invitations/reducer";

export const reducer = combineReducers({
  clans: clansReducer,
  clan: clanReducer,
  members: membersReducer,
  candidatures: candidaturesReducer,
  invitations: invitationsReducer
});
