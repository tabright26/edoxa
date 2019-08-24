import React from "react";
import { Route } from "react-router-dom";
import { connect } from "react-redux";
import userManager, { POST_LOGIN_REDIRECT_URI } from "../../../../utils/userManager";
import Loading from "../../../../components/Loading";

const SecureRoute = ({ isSignedIn, path, exact, name, scopes = [], component: Component }) => {
  return (
    <Route
      path={path}
      exact={exact}
      name={name}
      render={props => {
        if (!isSignedIn) {
          localStorage.setItem(POST_LOGIN_REDIRECT_URI, path);
          userManager.signinRedirect();
          return <Loading.Default />;
        }
        if (false) {
        }
        return <Component {...props} />;
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
