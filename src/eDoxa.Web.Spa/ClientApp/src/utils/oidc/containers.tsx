import React, { FunctionComponent } from "react";
import { connect, MapStateToProps } from "react-redux";
import {
  COUNTRY_CLAIM_TYPE,
  SUB_CLAIM_TYPE,
  BIRTHDATE_CLAIM_TYPE
} from "utils/oidc/types";
import { RootState } from "store/types";
import { UserId, Game, UserDob } from "types";
import { camelCase } from "change-case";

export interface HocUserProfileDobStateProps {
  dob: UserDob;
}

export const withUserProfileDob = (WrappedComponent: FunctionComponent) => {
  interface OwnProps {}

  const EnhancedComponent: FunctionComponent<HocUserProfileDobStateProps> = props => {
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

  return connect(mapStateToProps)(EnhancedComponent);
};

export interface HocUserProfileUserIdStateProps {
  userId: UserId;
}

export const withUserProfileUserId = (WrappedComponent: FunctionComponent) => {
  interface OwnProps {}

  const EnhancedComponent: FunctionComponent<HocUserProfileUserIdStateProps> = props => {
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

  return connect(mapStateToProps)(EnhancedComponent);
};

export interface HocUserProfileGameIsAuthenticatedStateProps {
  isAuthenticated: boolean;
}

export const withUserProfileGameIsAuthenticated = (
  WrappedComponent: FunctionComponent
) => {
  interface OwnProps {
    readonly game: Game;
  }

  const EnhancedComponent: FunctionComponent<HocUserProfileGameIsAuthenticatedStateProps> = props => {
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

  return connect(mapStateToProps)(EnhancedComponent);
};

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
