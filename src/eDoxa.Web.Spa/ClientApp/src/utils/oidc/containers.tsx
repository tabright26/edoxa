import React, { FunctionComponent } from "react";
import { connect, MapStateToProps } from "react-redux";
import { COUNTRY_CLAIM_TYPE } from "utils/oidc/types";
import { RootState } from "store/types";

export interface HocUserProfileCountryStateProps {
  country: string;
}

export const withUserProfileCountry = (WrappedComponent: FunctionComponent) => {
  interface OwnProps {}

  const EnhancedComponent: FunctionComponent<HocUserProfileCountryStateProps> = props => {
    return <WrappedComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<
    HocUserProfileCountryStateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      country: state.oidc.user.profile[COUNTRY_CLAIM_TYPE]
    };
  };

  return connect(mapStateToProps)(EnhancedComponent);
};

export interface HocUserIsAuthenticatedStateProps {
  isAuthenticated: boolean;
}

export const withUserIsAuthenticated = (
  WrappedComponent: FunctionComponent
) => {
  interface OwnProps {}

  const EnhancedComponent: FunctionComponent<HocUserIsAuthenticatedStateProps> = props => {
    return <WrappedComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<
    HocUserIsAuthenticatedStateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      isAuthenticated: !!state.oidc.user
    };
  };

  return connect(mapStateToProps)(EnhancedComponent);
};
