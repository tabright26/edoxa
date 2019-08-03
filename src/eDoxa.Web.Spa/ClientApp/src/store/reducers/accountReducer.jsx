import { FIND_ACCOUNT_BALANCE_SUCCESS } from '../actions/cashierActions';

const account = {
  money: { available: null, pending: null },
  token: { available: null, pending: null }
};

export const reducer = (state = account, action) => {
  switch (action.type) {
    case FIND_ACCOUNT_BALANCE_SUCCESS:
      const { balance } = action;
      const { currency, available, pending } = balance;
      const normalizedCurrency = currency.toLowerCase();
      if (normalizedCurrency === 'money') {
        state.money = { available, pending };
      }
      if (normalizedCurrency === 'token') {
        state.token = { available, pending };
      }
      return state;
    default:
      return state;
  }
};
