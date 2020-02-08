import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { SUB_CLAIM_TYPE } from "utils/oidc/types";
import { RootState } from "store/types";
import { compose } from "recompose";
import { HocUserProfileUserIdStateProps, mapDispatchToProps } from "../types";
import { withUserIsAuthenticated } from "../withUserIsAuthenticated";

export const withUserProfileUserId = (WrappedComponent: FunctionComponent) => {
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
    HocUserProfileUserIdStateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      userId: state.oidc.user.profile[SUB_CLAIM_TYPE]
    };
  };

  const enhance = compose(
    withUserIsAuthenticated,
    connect(mapStateToProps, mapDispatchToProps)
  );

  return enhance(EnhancedComponent);
};
