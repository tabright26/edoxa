import React, { FunctionComponent, useEffect } from "react";
import { connect, MapDispatchToProps } from "react-redux";
import { compose } from "recompose";
import { withUserIsAuthenticated } from "utils/oidc/containers";
import { HocUserIsAuthenticatedStateProps } from "utils/oidc/containers/types";
import { redirectToWorkflow } from "../constants";
import { withCookies, ReactCookieProps } from "react-cookie";

export const withWorkflow = (WrappedComponent: FunctionComponent) => {
  type OwnProps = HocUserIsAuthenticatedStateProps & ReactCookieProps;

  type DispatchProps = {
    redirectToWorkflow: () => void;
  };

  const EnhancedComponent: FunctionComponent<DispatchProps> = ({
    redirectToWorkflow,
    ...props
  }) => {
    useEffect(() => {
      redirectToWorkflow();
    }, [redirectToWorkflow]);
    return <WrappedComponent {...props} />;
  };

  const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
    dispatch,
    ownProps
  ) => {
    return {
      redirectToWorkflow: () => {
        if (ownProps.isAuthenticated) {
          redirectToWorkflow(ownProps.cookies, dispatch);
        }
      }
    };
  };

  const enhance = compose(
    withCookies,
    withUserIsAuthenticated,
    connect(null, mapDispatchToProps)
  );

  return enhance(EnhancedComponent);
};
