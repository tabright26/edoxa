import React, { FunctionComponent } from "react";
import { Switch, Redirect } from "react-router-dom";
import { routes } from "utils/coreui/_routes";
import Route from "utils/router/components/Route";
import AuthorizeRoute from "utils/oidc/AuthorizeRoute";
import { getError404Path } from "utils/coreui/constants";

const Routes: FunctionComponent = () => (
  <Switch>
    {routes
      .filter(route => route.component && !route.disabled)
      .map(({ allowAnonymous, ...route }, index: number) =>
        allowAnonymous ? (
          <Route.AllowAnonymous key={index} {...route} />
        ) : (
          <AuthorizeRoute key={index} {...route} />
        )
      )}
    <Redirect to={getError404Path()} />
  </Switch>
);

export default Routes;
