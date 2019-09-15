import { combineReducers } from "redux";

import { reducer as personalInfo } from "./personalInfo/reducer";
import { reducer as doxaTagHistory } from "./doxaTagHistory/reducer";
import { reducer as addressBook } from "./addressBook/reducer";
import { reducer as account } from "./account/reducer";
import { reducer as games } from "./games/reducer";

export const reducer = combineReducers({
  account,
  personalInfo,
  doxaTagHistory,
  addressBook,
  games
});
