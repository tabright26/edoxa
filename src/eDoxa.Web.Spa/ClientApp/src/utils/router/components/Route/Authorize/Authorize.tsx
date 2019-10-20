import React, { FunctionComponent, ExoticComponent } from "react";
import { Route, Redirect } from "react-router-dom";
import userManager, { POST_LOGIN_REDIRECT_URI } from "utils/oidc/userManager";
import Loading from "components/Shared/Loading";
import { RouteProps } from "utils/router/types";
import { withUserIsAuthenticated } from "store/root/user/container";
import { compose } from "recompose";

interface InnerProps {
  isAuthenticated: boolean;
}

interface OutterProps {
  path: string;
  exact: boolean;
  name: string;
  component: ExoticComponent<any>;
  scopes: string[];
}

type Props = InnerProps & OutterProps;

const AuthorizeRoute: FunctionComponent<Props> = ({ isAuthenticated, path, exact, name, scopes = [], component: Component }) => (
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
        return <Redirect to="/errors/401" />;
      }
      return <Component {...props} />;
    }}
  />
);

const enhance = compose<InnerProps, OutterProps>(withUserIsAuthenticated);

export default enhance(AuthorizeRoute);
