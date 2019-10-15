import {
  LOAD_DEPOSIT_MONEY_AMOUNTS,
  LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS,
  LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL,
  LOAD_DEPOSIT_TOKEN_AMOUNTS,
  LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS,
  LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL,
  DEPOSIT_MONEY_SUCCESS,
  DEPOSIT_MONEY_FAIL,
  DEPOSIT_TOKEN_SUCCESS,
  DEPOSIT_TOKEN_FAIL,
  DepositState,
  DepositActionTypes
} from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Currency } from "../types";
import { Reducer } from "redux";

export const initialState: DepositState = {
  data: { amounts: new Map<Currency, number[]>() },
  error: null,
  loading: false
};

export const reducer: Reducer<DepositState, DepositActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_DEPOSIT_MONEY_AMOUNTS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS: {
      const amounts = new Map<Currency, number[]>(state.data.amounts);
      amounts.set("money", action.payload.data);
      return { data: { amounts }, error: null, loading: false };
    }
    case LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case LOAD_DEPOSIT_TOKEN_AMOUNTS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS: {
      const amounts = new Map<Currency, number[]>(state.data.amounts);
      amounts.set("token", action.payload.data);
      return { data: { amounts }, error: null, loading: false };
    }
    case LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case DEPOSIT_MONEY_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case DEPOSIT_MONEY_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case DEPOSIT_TOKEN_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case DEPOSIT_TOKEN_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
