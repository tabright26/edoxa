import { combineReducers } from "redux";

import { reducer as paymentReducer } from "store/root/payment/reducer";
import { reducer as arenaReducer } from "store/root/arena/reducer";
import { reducer as userReducer } from "store/root/user/reducer";
import { reducer as organizationsReducer } from "store/root/organizations/reducer";
import { reducer as doxatagsReducer } from "store/root/doxatags/reducer";
import { reducer as gamesReducer } from "store/root/games/reducer";

export const reducer = combineReducers({
  payment: paymentReducer,
  arena: arenaReducer,
  user: userReducer,
  doxatags: doxatagsReducer,
  organizations: organizationsReducer,
  games: gamesReducer
});
