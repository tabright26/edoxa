import { AxiosErrorData } from "interfaces/axios";
import { SubmissionError } from "redux-form";
import { LOAD_WITHDRAWAL_MONEY_AMOUNTS_SUCCESS, LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL, WITHDRAWAL_TOKEN_SUCCESS, WITHDRAWAL_TOKEN_FAIL, WithdrawalState, WithdrawalActionTypes } from "./types";
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
    case WITHDRAWAL_TOKEN_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    }
    case WITHDRAWAL_TOKEN_SUCCESS:
    case LOAD_WITHDRAWAL_MONEY_AMOUNTS_FAIL:
    default: {
      return state;
    }
  }
};
