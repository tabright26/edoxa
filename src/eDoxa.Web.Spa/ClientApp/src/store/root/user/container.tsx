import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { confirmUserEmail } from "store/root/user/email/actions";
import { RootState } from "store/root/types";
import { ClaimType } from "store/middlewares/oidc/types";

export const withtUser = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => <HighOrderComponent {...props} />;

  const mapStateToProps = (state: RootState) => {
    const user = state.oidc.user;
    return {
      user: user,
      isAuthenticated: user
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        confirmEmail: (userId: string, code: string) => dispatch(confirmUserEmail(userId, code))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export const withtUserProfile = (claimType: ClaimType) => (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => <HighOrderComponent {...props} />;

  const mapStateToProps = (state: RootState) => {
    return {
      [claimType]: state.oidc.user.profile[claimType]
    };
  };

  return connect(mapStateToProps)(Container);
};
