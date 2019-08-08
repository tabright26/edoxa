import React from "react";
import { Route } from "react-router-dom";
import { connect } from "react-redux";
import userManager, { POST_LOGIN_REDIRECT_URI } from "../../../utils/userManager";

function SecureRoute({ isAuthorize, component: Component, path, ...attributes }) {
  return (
    <Route
      {...attributes}
      render={props => {
        if (!isAuthorize) {
          localStorage.setItem(POST_LOGIN_REDIRECT_URI, path);
          return userManager.signinRedirect();
        }
        return <Component {...props} />;
      }}
    />
  );
}

const mapStateToProps = state => {
  return {
    isAuthorize: state.oidc.user
  };
};

export default connect(mapStateToProps)(SecureRoute);
