import { LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS, LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL, WITHDRAWAL_TOKEN_SUCCESS, WITHDRAWAL_TOKEN_FAIL, WithdrawalState, WithdrawalActionTypes } from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Currency } from "../types";

export const initialState: WithdrawalState = {
  amounts: new Map<Currency, number[]>()
};

export const reducer = (state = initialState, action: WithdrawalActionTypes): WithdrawalState => {
  switch (action.type) {
    case LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS: {
      const amounts = new Map<Currency, number[]>(state.amounts);
      amounts.set("money", action.payload.data);
      return { amounts };
    }
    case LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL: {
      return state;
    }
    case WITHDRAWAL_TOKEN_SUCCESS: {
      return state;
    }
    case WITHDRAWAL_TOKEN_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
