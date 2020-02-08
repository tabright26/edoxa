import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { COUNTRY_CLAIM_TYPE } from "utils/oidc/types";
import { RootState } from "store/types";
import { compose } from "recompose";
import { HocUserProfileCountryStateProps, mapDispatchToProps } from "../types";
import { withUserIsAuthenticated } from "../withUserIsAuthenticated";

export const withUserProfileCountry = (WrappedComponent: FunctionComponent) => {
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
    HocUserProfileCountryStateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      country: state.oidc.user.profile[COUNTRY_CLAIM_TYPE]
    };
  };

  const enhance = compose(
    withUserIsAuthenticated,
    connect(mapStateToProps, mapDispatchToProps)
  );

  return enhance(EnhancedComponent);
};
