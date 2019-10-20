import React, { FunctionComponent } from "react";
import { Switch } from "react-router-dom";
import { routes } from "utils/router/routes";
import Route from "utils/router/components/Route";

const Routes: FunctionComponent = () => (
  <Switch>
    {routes
      .filter(route => route.component && !route.disabled)
      .map(({ allowAnonymous, ...route }, index: number) => (allowAnonymous ? <Route.AllowAnonymous key={index} {...route} /> : <Route.Authorize key={index} {...route} />))}
  </Switch>
);

export default Routes;
