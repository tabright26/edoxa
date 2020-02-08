import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { compose } from "recompose";
import { HocUserStateProps, mapDispatchToProps } from "../types";
import { withUserIsAuthenticated } from "../withUserIsAuthenticated";

export const withUser = (WrappedComponent: FunctionComponent) => {
    interface OwnProps {}
  
    const EnhancedComponent: FunctionComponent<any> = ({
      redirectToLogin,
      ...props
    }) => {
      useEffect(() => {
        redirectToLogin();
      }, [redirectToLogin]);
      return <WrappedComponent {...props} />;
    };
    const mapStateToProps: MapStateToProps<
      HocUserStateProps,
      OwnProps,
      RootState
    > = state => {
      return {
        user: state.oidc.user
      };
    };
  
    const enhance = compose(
      withUserIsAuthenticated,
      connect(mapStateToProps, mapDispatchToProps)
    );
  
    return enhance(EnhancedComponent);
  };