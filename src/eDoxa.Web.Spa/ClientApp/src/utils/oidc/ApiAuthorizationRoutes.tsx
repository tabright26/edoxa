import React, { Component, Fragment } from "react";
import { Route } from "react-router";
import { Login } from "./Login";
import { Logout } from "./Logout";
import {
  ApplicationPaths,
  LoginActions,
  LogoutActions
} from "./ApiAuthorizationConstants";

export default class ApiAuthorizationRoutes extends Component<any, any> {
  render() {
    return (
      <Fragment>
        <Route
          path={ApplicationPaths.Login}
          render={() => loginAction(LoginActions.Login)}
        />
        <Route
          path={ApplicationPaths.LoginFailed}
          render={() => loginAction(LoginActions.LoginFailed)}
        />
        <Route
          path={ApplicationPaths.LoginCallback}
          render={() => loginAction(LoginActions.LoginCallback)}
        />
        <Route
          path={ApplicationPaths.LogOut}
          render={() => logoutAction(LogoutActions.Logout)}
        />
        <Route
          path={ApplicationPaths.LogOutCallback}
          render={() => logoutAction(LogoutActions.LogoutCallback)}
        />
        <Route
          path={ApplicationPaths.LoggedOut}
          render={() => logoutAction(LogoutActions.LoggedOut)}
        />
      </Fragment>
    );
  }
}

function loginAction(name: string) {
  return <Login action={name} />;
}

function logoutAction(name: string) {
  return <Logout action={name} />;
}
