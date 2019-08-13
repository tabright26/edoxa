import { combineReducers } from "redux";

import { reducer as userPersonalInfoReducer } from "./userPersonalInfoReducer";
import { reducer as userDoxaTagReducer } from "./userDoxaTagReducer";
import { reducer as userAddressBookReducer } from "./userAddressBookReducer";
import { reducer as userAccountReducer } from "./userAccountReducer";
import { reducer as userGamesReducer } from "./userGamesReducer";

export const reducer = combineReducers({
  account: userAccountReducer,
  personalInfo: userPersonalInfoReducer,
  doxaTag: userDoxaTagReducer,
  addressBook: userAddressBookReducer,
  games: userGamesReducer
});
