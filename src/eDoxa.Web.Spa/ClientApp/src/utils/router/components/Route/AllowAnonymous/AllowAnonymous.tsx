import React, { FunctionComponent, ExoticComponent } from "react";
import { Route } from "react-router-dom";
import { RouteProps } from "utils/router/types";

interface Props {
  path: string;
  exact: boolean;
  name: string;
  component: ExoticComponent<any>;
}

const AllowAnonymousRoute: FunctionComponent<Props> = ({ path, exact, name, component: Component }) => (
  <Route<RouteProps> path={path} exact={exact} name={name} render={props => <Component {...props} />} />
);

export default AllowAnonymousRoute;
