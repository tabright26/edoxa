import React from "react";
import { Switch } from "react-router-dom";

import AllowAnonymous from "./AllowAnonymous";
import Authorize from "./Authorize";

const RouteHandler = ({ routes }) => (
  <Switch>{routes.filter(route => route.component).map(({ secure, ...route }, index) => (secure ? <Authorize key={index} {...route} /> : <AllowAnonymous key={index} {...route} />))}</Switch>
);

export default RouteHandler;
