import { combineReducers } from "redux";

import { reducer as userPersonalInfoReducer } from "./userPersonalInfoReducer";
import { reducer as userDoxaTagReducer } from "./userDoxaTagReducer";
import { reducer as userAddressReducer } from "./userAddressReducer";
import { reducer as userAccountReducer } from "./userAccountReducer";
import { reducer as userGamesReducer } from "./userGamesReducer";

export const reducer = combineReducers({
  account: userAccountReducer,
  personalInfo: userPersonalInfoReducer,
  doxaTag: userDoxaTagReducer,
  address: userAddressReducer,
  games: userGamesReducer
});
