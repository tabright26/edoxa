import {
  LOAD_WITHDRAWAL_MONEY_AMOUNTS,
  LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS,
  LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL,
  WITHDRAWAL_TOKEN_SUCCESS,
  WITHDRAWAL_TOKEN_FAIL,
  WithdrawalState,
  WithdrawalActionTypes
} from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Currency } from "../types";
import { Reducer } from "redux";

export const initialState: WithdrawalState = {
  data: { amounts: new Map<Currency, number[]>() },
  error: null,
  loading: false
};

export const reducer: Reducer<WithdrawalState, WithdrawalActionTypes> = (state = initialState, action) => {
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
      return { data: state.data, error: LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL, loading: false };
    }
    case WITHDRAWAL_TOKEN_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case WITHDRAWAL_TOKEN_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
