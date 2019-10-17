import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { ClaimType } from "store/middlewares/oidc/types";

export const withUser = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => <HighOrderComponent {...props} />;

  const mapStateToProps = (state: RootState) => {
    return {
      user: state.oidc.user
    };
  };

  return connect(mapStateToProps)(Container);
};

export const withUserIsAuthenticated = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => <HighOrderComponent {...props} />;

  const mapStateToProps = (state: RootState) => {
    const isAuthenticated: boolean = !!state.oidc.user;
    return {
      isAuthenticated
    };
  };

  return connect(mapStateToProps)(Container);
};

export const withtUserProfile = (claimType: ClaimType) => (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => <HighOrderComponent {...props} />;

  const mapStateToProps = (state: RootState) => {
    return {
      [claimType]: state.oidc.user.profile[claimType] || null
    };
  };

  return connect(mapStateToProps)(Container);
};
