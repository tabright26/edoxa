import axios from "axios";
import axiosMiddleware from "redux-axios-middleware";
import authorizeService from "utils/oidc/AuthorizeService";
import { async } from "q";

export const middleware = axiosMiddleware(
  axios.create({
    baseURL: process.env.REACT_APP_CHALLENGES_WEB_GATEWAY_URL,
    responseType: "json"
  }),
  {
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
  }
);
