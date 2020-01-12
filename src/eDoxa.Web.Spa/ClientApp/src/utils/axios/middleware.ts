import axios from "axios";
import { multiClientMiddleware, IClientsList } from "redux-axios-middleware";
import authorizeService from "utils/oidc/AuthorizeService";
import {
  REACT_APP_CASHIER_WEB_GATEWAY_URL,
  REACT_APP_CHALLENGES_WEB_GATEWAY_URL
} from "keys";

const clients: IClientsList = {
  default: {
    client: axios.create({
      baseURL: REACT_APP_CHALLENGES_WEB_GATEWAY_URL,
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
        var accessToken = await authorizeService.getAccessToken();
        if (accessToken) {
          config.headers["Authorization"] = `Bearer ${accessToken}`;
        }
        return config;
      }
    ]
  }
});
