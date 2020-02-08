import React, { FunctionComponent } from "react";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { HocUserIsAuthenticatedStateProps } from "../types";

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
