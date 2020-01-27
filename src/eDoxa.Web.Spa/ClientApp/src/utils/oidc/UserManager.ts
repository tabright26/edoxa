import { UserManagerSettings, WebStorageStateStore } from "oidc-client";
import { ApplicationPaths } from "./ApiAuthorizationConstants";
import { REACT_APP_WEB_SPA, REACT_APP_AUTHORITY } from "keys";
import { createUserManager } from "redux-oidc";

const options: UserManagerSettings = {
  client_id: "web-spa",
  redirect_uri: REACT_APP_WEB_SPA + ApplicationPaths.LoginCallback,
  response_type: "token id_token",
  scope:
    "openid profile country stripe roles permissions games identity.api payment.api cashier.api challenges.api games.api clans.api challenges.web.aggregator cashier.web.aggregator",
  authority: REACT_APP_AUTHORITY,
  post_logout_redirect_uri: REACT_APP_WEB_SPA + ApplicationPaths.LogOutCallback,
  silent_redirect_uri: REACT_APP_WEB_SPA + ApplicationPaths.LoginCallback,
  filterProtocolClaims: true,
  automaticSilentRenew: true,
  includeIdTokenInSilentRenew: true,
  loadUserInfo: true,
  userStore: new WebStorageStateStore({
    prefix: "edoxa-"
  })
};

const userManager = createUserManager(options);

export { userManager };
