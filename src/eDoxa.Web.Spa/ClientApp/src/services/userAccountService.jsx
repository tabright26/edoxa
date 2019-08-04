import axios from 'axios';

export async function loadUserAccountBalanceMoneyAsync(getState) {
  return await loadUserAccountBalanceAsync('money', getState);
}

export async function loadUserAccountBalanceTokenAsync(getState) {
  return await loadUserAccountBalanceAsync('token', getState);
}

async function loadUserAccountBalanceAsync(currency, getState) {
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

export async function loadUserAccountTransactionsAsync(getState) {
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

export async function loadUserStripeCardsAsync(getState) {
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

export async function loadUserStripeBankAccountsAsync(getState) {
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
