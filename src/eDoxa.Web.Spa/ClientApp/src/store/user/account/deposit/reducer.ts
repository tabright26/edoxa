import { AxiosErrorData } from "interfaces/axios";
import { SubmissionError } from "redux-form";
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
    case LOAD_DEPOSIT_TOKEN_AMOUNTS_SUCCESS: {
      const amounts = new Map<Currency, number[]>(state.amounts);
      amounts.set("token", action.payload.data);
      return { amounts };
    }
    case DEPOSIT_MONEY_FAIL:
    case DEPOSIT_TOKEN_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    }
    case DEPOSIT_MONEY_SUCCESS:
    case DEPOSIT_TOKEN_SUCCESS:
    case LOAD_DEPOSIT_MONEY_AMOUNTS_FAIL:
    case LOAD_DEPOSIT_TOKEN_AMOUNTS_FAIL:
    default: {
      return state;
    }
  }
};
