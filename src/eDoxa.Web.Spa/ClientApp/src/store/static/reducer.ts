import { Reducer } from "redux";
import produce, { Draft } from "immer";
import {
  StaticOptionsActions,
  LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
  LOAD_CASHIER_STATIC_OPTIONS_SUCCESS,
  LOAD_GAMES_STATIC_OPTIONS_SUCCESS,
  LOAD_CHALLENGES_STATIC_OPTIONS_SUCCESS
} from "store/actions/static/types";
import { StaticOptionsState } from "./types";

const initialState: StaticOptionsState = {
  identity: null,
  cashier: null,
  challenges: null,
  games: null
};

export const reducer: Reducer<
  StaticOptionsState,
  StaticOptionsActions
> = produce(
  (draft: Draft<StaticOptionsState>, action: StaticOptionsActions) => {
    switch (action.type) {
      case LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS: {
        draft.identity = action.payload.data;
        break;
      }
      case LOAD_CASHIER_STATIC_OPTIONS_SUCCESS: {
        draft.cashier = action.payload.data;
        break;
      }
      case LOAD_CHALLENGES_STATIC_OPTIONS_SUCCESS: {
        draft.challenges = action.payload.data;
        break;
      }
      case LOAD_GAMES_STATIC_OPTIONS_SUCCESS: {
        draft.games = action.payload.data;
        break;
      }
    }
  },
  initialState
);
