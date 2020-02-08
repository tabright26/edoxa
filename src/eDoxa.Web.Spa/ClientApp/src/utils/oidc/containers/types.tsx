import { MapDispatchToProps } from "react-redux";
import {
  ApplicationPaths,
  QueryParameterNames
} from "utils/oidc/ApiAuthorizationConstants";
import { User } from "oidc-client";
import { UserDob, UserId } from "types";
import { push } from "react-router-redux";

interface DispatchProps {
  redirectToLogin: () => void;
}

export const mapDispatchToProps: MapDispatchToProps<
  DispatchProps,
  HocUserIsAuthenticatedStateProps
> = (dispatch, ownProps) => {
  return {
    redirectToLogin: () => {
      const redirectUrl = `${ApplicationPaths.Login}?${
        QueryParameterNames.ReturnUrl
      }=${encodeURI(window.location.href)}`;
      if (!ownProps.isAuthenticated) {
        dispatch(push(redirectUrl));
      }
    }
  };
};

export interface HocUserStateProps {
  user: User;
}
export interface HocUserProfileDobStateProps {
  dob: UserDob;
}
export interface HocUserProfileUserIdStateProps {
  userId: UserId;
}
export interface HocUserProfileGameIsAuthenticatedStateProps {
  isAuthenticated: boolean;
}
export interface HocUserProfileCountryStateProps {
  country: string;
}
export interface HocUserIsAuthenticatedStateProps {
  isAuthenticated: boolean;
}
