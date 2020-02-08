import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { Game } from "types";
import { camelCase } from "change-case";
import { compose } from "recompose";
import { HocUserIsAuthenticatedStateProps, HocUserProfileGameIsAuthenticatedStateProps, mapDispatchToProps } from "../types";
import { withUserIsAuthenticated } from "../withUserIsAuthenticated";

export const withUserProfileGameIsAuthenticated = (
    WrappedComponent: FunctionComponent
  ) => {
    interface OwnProps extends HocUserIsAuthenticatedStateProps {
      readonly game: Game;
    }
  
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
      HocUserProfileGameIsAuthenticatedStateProps,
      OwnProps,
      RootState
    > = (state, ownProps) => {
      return {
        isAuthenticated: !!state.oidc.user.profile[
          `games:${camelCase(ownProps.game)}`
        ]
      };
    };
  
    const enhance = compose(
      withUserIsAuthenticated,
      connect(mapStateToProps, mapDispatchToProps)
    );
  
    return enhance(EnhancedComponent);
  };