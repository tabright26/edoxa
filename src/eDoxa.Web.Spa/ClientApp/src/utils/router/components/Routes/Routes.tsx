import React, { FunctionComponent } from "react";
import { Switch, Redirect } from "react-router-dom";
import { routes } from "utils/coreui/_routes";
import Route from "components/Shared/Route";
import {
  getChallengesPath,
  getDefaultPath,
  getHomePath,
  getError404Path
} from "utils/coreui/constants";
import { HocUserIsAuthenticatedStateProps } from "utils/oidc/containers/types";
import { compose } from "redux";
import { withUserIsAuthenticated } from "utils/oidc/containers";

type Props = HocUserIsAuthenticatedStateProps;

const Routes: FunctionComponent<Props> = ({ isAuthenticated }) => (
  <Switch>
    {routes
      .filter(route => route.component && !route.disabled)
      .map(({ allowAnonymous, ...route }, index) =>
        allowAnonymous ? (
          <Route.AllowAnonymous key={index} {...route} />
        ) : (
          <Route.Authorize key={index} {...route} />
        )
      )}
    {isAuthenticated ? (
      <Redirect exact from={getDefaultPath()} to={getChallengesPath()} />
    ) : (
      <Redirect exact from={getDefaultPath()} to={getHomePath()} />
    )}
    <Redirect to={getError404Path()} />
  </Switch>
);

const enhance = compose(withUserIsAuthenticated);

export default enhance(Routes);
