import axios from "axios";
import axiosMiddleware from "redux-axios-middleware";

export const middleware = axiosMiddleware(
  axios.create({
    baseURL: process.env.REACT_APP_CHALLENGES_WEB_GATEWAY,
    responseType: "json"
  }),
  {
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
);
