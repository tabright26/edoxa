import axios from 'axios';

export async function findAccountBalanceAsync(currency, getState) {
  const state = getState();
  const { access_token } = state.oidc.user;
  return await axios({
    method: 'get',
    url: `${process.env.REACT_APP_WEB_GATEWAY}/cashier/api/account/balance/${currency}`,
    headers: {
      authorization: `Bearer ${access_token}`,
      accept: 'application/json'
    }
  });
}

export async function fetchTransactionsAsync(getState) {
  const state = getState();
  const { access_token } = state.oidc.user;
  return await axios({
    method: 'get',
    url: `${process.env.REACT_APP_WEB_GATEWAY}/cashier/api/transactions`,
    headers: {
      authorization: `Bearer ${access_token}`,
      accept: 'application/json'
    }
  });
}

export async function fetchCardsAsync(getState) {
  const state = getState();
  const { profile } = state.oidc.user;
  return await axios({
    method: 'get',
    url: `https://api.stripe.com/v1/customers/${
      profile['stripe:customerId']
    }/sources?object=card`,
    headers: {
      authorization: `Bearer ${process.env.REACT_APP_STRIPE_APIKEY}`,
      accept: 'application/json'
    }
  });
}

export async function hasBankAccountAsync(getState) {
  const state = getState();
  const { profile } = state.oidc.user;
  return await axios({
    method: 'get',
    url: `https://api.stripe.com/v1/customers/${
      profile['stripe:customerId']
    }/sources?object=bank_account`,
    headers: {
      authorization: `Bearer ${process.env.REACT_APP_STRIPE_APIKEY}`,
      accept: 'application/json'
    }
  });
}
