import { combineReducers } from "redux";

import { reducer as bankAccountsReducer } from "./bankAccounts/reducer";
import { reducer as cardsReducer } from "./cards/reducer";

export const reducer = combineReducers({
  bankAccounts: bankAccountsReducer,
  cards: cardsReducer
});
