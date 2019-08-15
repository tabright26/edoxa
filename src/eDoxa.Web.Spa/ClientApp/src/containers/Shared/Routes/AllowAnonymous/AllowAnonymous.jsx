import React from "react";
import { Route } from "react-router-dom";

const AllowAnonymousRoute = ({ path, exact, name, modals = [], component: Component }) => {
  return (
    <Route
      path={path}
      exact={exact}
      name={name}
      render={props => {
        return (
          <>
            {modals.map((Modal, index) => (
              <Modal key={index} />
            ))}
            <Component {...props} />
          </>
        );
      }}
    />
  );
};

export default AllowAnonymousRoute;
