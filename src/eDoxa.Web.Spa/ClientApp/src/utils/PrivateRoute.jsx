import React from 'react';
import { Route } from 'react-router-dom';
import { connect } from 'react-redux';
import userManager, { POST_LOGIN_REDIRECT_URI } from './userManager';

const PrivateRoute = ({
  isAuthenticated,
  isLoadingUser,
  component,
  path,
  ...rest
}) => {
  if (!isLoadingUser && !isAuthenticated) {
    localStorage.setItem(POST_LOGIN_REDIRECT_URI, path);
    userManager.signinRedirect();
  }
  return isAuthenticated ? <Route {...rest} component={component} /> : <></>;
};

const mapStateToProps = state => {
  return {
    isAuthenticated: state.oidc.user,
    isLoadingUser: state.oidc.isLoadingUser
  };
};

export default connect(mapStateToProps)(PrivateRoute);
