import React, { FunctionComponent } from "react";
import { Route } from "react-router-dom";
import userManager, { POST_LOGIN_REDIRECT_URI } from "store/middlewares/oidc/userManager";
import Loading from "components/Shared/Override/Loading";
import { RouteProps } from "store/middlewares/router/types";
import { withUserIsAuthenticated } from "store/root/user/container";
import { compose } from "recompose";

const SecureRoute: FunctionComponent<any> = ({ isAuthenticated, path, exact, name, scopes = [], component: Component }) => (
  <Route<RouteProps>
    path={path}
    exact={exact}
    name={name}
    render={props => {
      if (!isAuthenticated) {
        localStorage.setItem(POST_LOGIN_REDIRECT_URI, path);
        userManager.signinRedirect();
        return <Loading />;
      }
      if (false) {
      }
      return <Component {...props} />;
    }}
  />
);

const enhance = compose<any, any>(withUserIsAuthenticated);

export default enhance(SecureRoute);
