import React from "react";
import { Route } from "react-router-dom";
import { connect } from "react-redux";
import userManager, { POST_LOGIN_REDIRECT_URI } from "../../../../utils/userManager";

const SecureRoute = ({ isSignedIn, path, exact, name, modals = [], scopes = [], component: Component }) => {
  return (
    <Route
      path={path}
      exact={exact}
      name={name}
      render={props => {
        if (!isSignedIn) {
          localStorage.setItem(POST_LOGIN_REDIRECT_URI, path);
          userManager.signinRedirect();
        }
        if (true) {
        }
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

const mapStateToProps = state => {
  return {
    isSignedIn: state.oidc.user
  };
};

export default connect(mapStateToProps)(SecureRoute);
