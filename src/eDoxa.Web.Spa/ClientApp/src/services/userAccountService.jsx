import axios from 'axios';

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
