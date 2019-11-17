import { ExoticComponent } from "react";
import * as Router from "react-router-dom";

export interface RouteProps extends Router.RouteProps {
  name: string;
}

export interface RouteConfig {
  readonly path: string;
  readonly name: string;
  readonly exact: boolean;
  readonly allowAnonymous: boolean;
  readonly disabled: boolean;
  readonly scopes: string[];
  readonly component: ExoticComponent;
}
