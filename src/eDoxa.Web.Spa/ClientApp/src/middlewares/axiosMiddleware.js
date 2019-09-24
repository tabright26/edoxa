import axios from "axios";
import queryString from "query-string";
import { multiClientMiddleware } from "redux-axios-middleware";

export const middleware = multiClientMiddleware({
  default: {
    client: axios.create({
      baseURL: process.env.REACT_APP_WEB_GATEWAY,
      responseType: "json"
    }),
    options: {
      interceptors: {
        request: [
          ({ getState }, config) => {
            const state = getState();
            const { user } = state.oidc;
            if (user) {
              config.headers["Authorization"] = `Bearer ${user.access_token}`;
            }
            return config;
          }
        ]
      }
    }
  },
  stripe: {
    client: axios.create({
      baseURL: "https://api.stripe.com",
      responseType: "json"
    }),
    options: {
      interceptors: {
        request: [
          ({ getState }, config) => {
            const state = getState();
            const { profile } = state.oidc.user;
            config.url = config.url.replace(":customerId", profile["stripe:customerId"]).replace(":connectAccountId", profile["stripe:connectAccountId"]);
            config.headers = {
              "Content-Type": "application/x-www-form-urlencoded",
              authorization: `Bearer ${process.env.REACT_APP_STRIPE_APIKEYS_SECRETKEY}`
            };
            config.data = queryString.stringify(config.data);
            return config;
          }
        ]
      }
    }
  },
  leagueOfLegends: {
    client: axios.create({
      baseURL: "https://na1.api.riotgames.com",
      responseType: "json"
    }),
    options: {
      interceptors: {
        request: [
          ({ getState }, config) => {
            const state = getState();
            const user = state.oidc;
            if (user) {
              config.headers = {
                "X-Riot-Token": process.env.REACT_APP_LEAGUEOFLEGENDS_RIOT_TOKEN,
                "Accept-Charset": "application/x-www-form-urlencoded; charset=UTF-8"
              };
            }
            return config;
          }
        ]
      }
    }
  }
});
