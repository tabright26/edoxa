import {
  LOAD_WITHDRAWAL_MONEY_AMOUNTS,
  LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS,
  LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL,
  WITHDRAWAL_MONEY,
  WITHDRAWAL_MONEY_SUCCESS,
  WITHDRAWAL_MONEY_FAIL,
  WithdrawalState,
  WithdrawalActions
} from "./types";
import { Currency } from "types";
import { Reducer } from "redux";

export const initialState: WithdrawalState = {
  data: { amounts: new Map<Currency, number[]>() },
  error: null,
  loading: false
};

export const reducer: Reducer<WithdrawalState, WithdrawalActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_WITHDRAWAL_MONEY_AMOUNTS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS: {
      const amounts = new Map<Currency, number[]>(state.data.amounts);
      amounts.set("money", action.payload.data);
      return { data: { amounts }, error: null, loading: false };
    }
    case LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case WITHDRAWAL_MONEY: {
      return { data: state.data, error: null, loading: true };
    }
    case WITHDRAWAL_MONEY_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case WITHDRAWAL_MONEY_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
