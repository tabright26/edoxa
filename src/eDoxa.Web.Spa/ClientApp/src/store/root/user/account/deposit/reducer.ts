import {
  LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS,
  LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL,
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

export const initialState: DepositState = {
  amounts: new Map<Currency, number[]>()
};

export const reducer = (state = initialState, action: DepositActionTypes): DepositState => {
  switch (action.type) {
    case LOAD_DEPOSIT_MONEY_AMOUNTS_SUCCESS: {
      const amounts = new Map<Currency, number[]>(state.amounts);
      amounts.set("money", action.payload.data);
      return { amounts };
    }
    case LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS: {
      const amounts = new Map<Currency, number[]>(state.amounts);
      amounts.set("token", action.payload.data);
      return { amounts };
    }
    case LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case DEPOSIT_MONEY_SUCCESS: {
      return state;
    }
    case DEPOSIT_MONEY_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case DEPOSIT_TOKEN_SUCCESS: {
      return state;
    }
    case DEPOSIT_TOKEN_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
