import axios from 'axios';

import { multiClientMiddleware } from 'redux-axios-middleware';

export const middleware = multiClientMiddleware({
  default: {
    client: axios.create({
      baseURL: process.env.REACT_APP_WEB_GATEWAY,
      responseType: 'json'
    })
  },
  stripe: {
    client: axios.create({
      baseURL: 'https://api.stripe.com',
      responseType: 'json'
    })
  },
  leagueOfLegends: {
    client: axios.create({
      baseURL: 'https://na1.api.riotgames.com',
      responseType: 'json'
    })
  }
});
