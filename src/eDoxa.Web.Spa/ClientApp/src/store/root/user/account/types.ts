import { AccountLogoutToken } from "types/identity";

export type UserAccountState = {
  logout: {
    token: AccountLogoutToken;
  };
};
