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
}

export const LoginActions: LoginActions = {
  Login: "login",
  LoginCallback: "login-callback",
  LoginFailed: "login-failed"
};

const prefix: string = "/authentication";

interface ApplicationPaths {
  readonly ApiAuthorizationPrefix: string;
  readonly Login: string;
  readonly LoginFailed: string;
  readonly LoginCallback: string;
  readonly LogOut: string;
  readonly LoggedOut: string;
  readonly LogOutCallback: string;
}

export const ApplicationPaths: ApplicationPaths = {
  ApiAuthorizationPrefix: prefix,
  Login: `${prefix}/${LoginActions.Login}`,
  LoginFailed: `${prefix}/${LoginActions.LoginFailed}`,
  LoginCallback: `${prefix}/${LoginActions.LoginCallback}`,
  LogOut: `${prefix}/${LogoutActions.Logout}`,
  LoggedOut: `${prefix}/${LogoutActions.LoggedOut}`,
  LogOutCallback: `${prefix}/${LogoutActions.LogoutCallback}`
};
