import React, { FunctionComponent } from "react";
import { Switch, Redirect } from "react-router-dom";
import { routes } from "utils/coreui/_routes";
import Route from "components/Shared/Route";
import { getError404Path } from "utils/coreui/constants";

const Routes: FunctionComponent = () => (
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
    <Redirect to={getError404Path()} />
  </Switch>
);

export default Routes;
