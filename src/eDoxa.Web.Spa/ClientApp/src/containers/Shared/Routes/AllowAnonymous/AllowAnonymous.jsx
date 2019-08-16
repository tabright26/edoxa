import React from "react";
import { Route } from "react-router-dom";

const AllowAnonymousRoute = ({ path, exact, name, component: Component }) => <Route path={path} exact={exact} name={name} render={props => <Component {...props} />} />;

export default AllowAnonymousRoute;
