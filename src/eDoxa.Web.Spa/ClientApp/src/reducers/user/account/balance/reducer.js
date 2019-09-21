import actionTypes from "actions/cashier";

export const initialState = { money: { type: "Money", available: 0, pending: 0 }, token: { type: "Token", available: 0, pending: 0 } };

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.LOAD_USER_ACCOUNT_BALANCE_SUCCESS:
      const { currency, available, pending } = action.payload.data;
      switch (currency) {
        case "Money":
          state.money = { type: currency, available, pending };
          return state;
        case "Token":
          state.token = { type: currency, available, pending };
          return state;
        default:
          return state;
      }
    case actionTypes.LOAD_USER_ACCOUNT_BALANCE_FAIL:
    default:
      return state;
  }
};
