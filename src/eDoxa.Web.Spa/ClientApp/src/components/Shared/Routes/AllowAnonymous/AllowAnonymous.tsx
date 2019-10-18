import React, { FunctionComponent } from "react";
import { Route } from "react-router-dom";
import { RouteProps } from "store/middlewares/router/types";

const AllowAnonymousRoute: FunctionComponent<any> = ({ path, exact, name, component: Component }) => (
  <Route<RouteProps> path={path} exact={exact} name={name} render={props => <Component {...props} />} />
);

export default AllowAnonymousRoute;
