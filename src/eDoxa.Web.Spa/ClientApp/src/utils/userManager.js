import { createUserManager } from "redux-oidc";

export const POST_LOGIN_REDIRECT_URI = "POST_LOGIN_REDIRECT_URI";

const userManagerConfig = {
  client_id: process.env.REACT_APP_CLIENT_ID,
  redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ""}/callback`,
  response_type: process.env.REACT_APP_RESPONSE_TYPE,
  scope: process.env.REACT_APP_SCOPE,
  authority: process.env.REACT_APP_AUTHORITY,
  post_logout_redirect_uri: window.location.origin,
  silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ""}/silent_renew.html`,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true
};

const userManager = createUserManager(userManagerConfig);

export default userManager;
