import { LogoutToken } from "types";

export type UserAccountState = {
  logout: {
    token: LogoutToken;
  };
};
