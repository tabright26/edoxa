import React, { FunctionComponent, ExoticComponent } from "react";
import { Route } from "react-router-dom";
import { RouteProps } from "utils/router/types";

interface Props {
  path: string;
  exact: boolean;
  name: string;
  component: ExoticComponent;
}

export const AllowAnonymous: FunctionComponent<Props> = ({
  path,
  exact,
  name,
  component: Component
}) => (
  <Route<RouteProps>
    path={path}
    exact={exact}
    name={name}
    component={Component}
  />
);
