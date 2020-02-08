import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { BIRTHDATE_CLAIM_TYPE } from "utils/oidc/types";
import { RootState } from "store/types";
import { compose } from "recompose";
import { HocUserProfileDobStateProps, mapDispatchToProps } from "../types";
import { withUserIsAuthenticated } from "../withUserIsAuthenticated";

export const withUserProfileDob = (WrappedComponent: FunctionComponent) => {
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
    HocUserProfileDobStateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      dob: JSON.parse(state.oidc.user.profile[BIRTHDATE_CLAIM_TYPE])
    };
  };

  const enhance = compose(
    withUserIsAuthenticated,
    connect(mapStateToProps, mapDispatchToProps)
  );

  return enhance(EnhancedComponent);
};
