import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { EMAIL_VERIFIED_CLAIM_TYPE } from "utils/oidc/types";
import { RootState } from "store/types";
import { compose } from "recompose";
import {
  mapDispatchToProps,
  HocUserProfileEmailVerifiedStateProps
} from "../types";
import { withUserIsAuthenticated } from "../withUserIsAuthenticated";

export const withUserProfileEmailVerified = (
  WrappedComponent: FunctionComponent
) => {
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
    HocUserProfileEmailVerifiedStateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      emailVerified: state.oidc.user.profile[EMAIL_VERIFIED_CLAIM_TYPE]
    };
  };

  const enhance = compose(
    withUserIsAuthenticated,
    connect(mapStateToProps, mapDispatchToProps)
  );

  return enhance(EnhancedComponent);
};
