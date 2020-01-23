import { REACT_APP_AUTHORITY } from "keys";
import { getUserProfileOverviewPath } from "utils/coreui/constants";

export const ApplicationName: string = "edoxa";

interface QueryParameterNames {
  ReturnUrl: string;
  Message: string;
}

export const QueryParameterNames: QueryParameterNames = {
  ReturnUrl: "returnUrl",
  Message: "message"
};

interface LogoutActions {
  LogoutCallback: string;
  Logout: string;
  LoggedOut: string;
}

export const LogoutActions: LogoutActions = {
  LogoutCallback: "logout-callback",
  Logout: "logout",
  LoggedOut: "logged-out"
};

interface LoginActions {
  Login: string;
  LoginCallback: string;
  LoginFailed: string;
  Profile: string;
  Register: string;
}

export const LoginActions: LoginActions = {
  Login: "login",
  LoginCallback: "login-callback",
  LoginFailed: "login-failed",
  Profile: "profile",
  Register: "register"
};

const prefix: string = "/authentication";

interface ApplicationPaths {
  readonly DefaultLoginRedirectPath: string;
  readonly ApiAuthorizationClientConfigurationUrl: string;
  readonly ApiAuthorizationPrefix: string;
  readonly Login: string;
  readonly LoginFailed: string;
  readonly LoginCallback: string;
  readonly Register: string;
  readonly Profile: string;
  readonly LogOut: string;
  readonly LoggedOut: string;
  readonly LogOutCallback: string;
  readonly IdentityRegisterPath: string;
  readonly IdentityManagePath: string;
}

export const ApplicationPaths: ApplicationPaths = {
  DefaultLoginRedirectPath: "/",
  ApiAuthorizationClientConfigurationUrl: `/_configuration/${ApplicationName}`,
  ApiAuthorizationPrefix: prefix,
  Login: `${prefix}/${LoginActions.Login}`,
  LoginFailed: `${prefix}/${LoginActions.LoginFailed}`,
  LoginCallback: `${prefix}/${LoginActions.LoginCallback}`,
  Register: `${REACT_APP_AUTHORITY}/Identity/Account/Register`,
  Profile: getUserProfileOverviewPath(),
  LogOut: `${prefix}/${LogoutActions.Logout}`,
  LoggedOut: `${prefix}/${LogoutActions.LoggedOut}`,
  LogOutCallback: `${prefix}/${LogoutActions.LogoutCallback}`,
  IdentityRegisterPath: "/Identity/Account/Register",
  IdentityManagePath: getUserProfileOverviewPath()
};
