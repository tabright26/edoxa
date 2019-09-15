import { combineReducers } from "redux";

import { reducer as challenges } from "./challenges/reducer";
import { reducer as games } from "./games/reducer";

import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage/session";

const persistConfig = {
  key: "arena",
  storage,
  blacklist: ["challenges"]
};

export const reducer = persistReducer(
  persistConfig,
  combineReducers({
    challenges,
    games
  })
);
