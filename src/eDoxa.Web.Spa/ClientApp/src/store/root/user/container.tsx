import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { push } from "connected-react-router";
import { confirmUserEmail } from "store/root/user/email/actions";
import { forgotUserPassword, resetUserPassword } from "store/root/user/password/actions";
import { RootState } from "store/root/types";

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
        confirmEmail: (userId: string, code: string) => dispatch(confirmUserEmail(userId, code)),
        forgotPassword: (fields: any) => dispatch(forgotUserPassword(fields)).then(() => dispatch(push("/"))),
        resetPassword: (fields: any, code: string) => {
          const data = fields;
          delete data.confirmPassword;
          data.code = code;
          return dispatch(resetUserPassword(data)).then(() => (window.location.href = `${process.env.REACT_APP_AUTHORITY}/account/login`));
        }
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export const withtUserProfile = (claimType: string) => (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => <HighOrderComponent {...props} />;

  const mapStateToProps = (state: RootState) => {
    return {
      [`${claimType}`]: state.oidc.user.profile[claimType]
    };
  };

  return connect(mapStateToProps)(Container);
};
