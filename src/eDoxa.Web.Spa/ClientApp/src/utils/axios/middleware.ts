import axios from "axios";
import { multiClientMiddleware, IClientsList } from "redux-axios-middleware";
import { RootState } from "store/types";
import {
  REACT_APP_CASHIER_WEB_GATEWAY_URL,
  REACT_APP_CHALLENGES_WEB_GATEWAY_URL,
  REACT_APP_AUTHORITY
} from "keys";

const clients: IClientsList = {
  default: {
    client: axios.create({
      baseURL: REACT_APP_CHALLENGES_WEB_GATEWAY_URL,
      responseType: "json"
    })
  },
  authority: {
    client: axios.create({
      baseURL: REACT_APP_AUTHORITY,
      responseType: "json"
    })
  },
  cashier: {
    client: axios.create({
      baseURL: REACT_APP_CASHIER_WEB_GATEWAY_URL,
      responseType: "json"
    })
  },
  challenges: {
    client: axios.create({
      baseURL: REACT_APP_CHALLENGES_WEB_GATEWAY_URL,
      responseType: "json"
    })
  }
};

export const middleware = multiClientMiddleware(clients, {
  interceptors: {
    request: [
      async ({ getState }, config) => {
        const state: RootState = getState();
        const user = state.oidc.user;
        if (user) {
          config.headers["Authorization"] = `Bearer ${user.access_token}`;
        }
        return config;
      }
    ]
  }
});
