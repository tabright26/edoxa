import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { push } from "connected-react-router";
import { confirmEmail } from "store/root/user/email/actions";
import { forgotPassword, resetPassword } from "store/root/user/password/actions";
import { RootState } from "store/root/types";

export const withtUser = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, user, isAuthenticated, ...attributes }) => (
    <ConnectedComponent actions={actions} user={user} isAuthenticated={isAuthenticated} {...attributes} />
  );

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
        confirmEmail: (userId: string, code: string) => dispatch(confirmEmail(userId, code)),
        forgotPassword: (fields: any) => dispatch(forgotPassword(fields)).then(() => dispatch(push("/"))),
        resetPassword: (fields: any, code: string) => {
          const data = fields;
          delete data.confirmPassword;
          data.code = code;
          return dispatch(resetPassword(data)).then(() => (window.location.href = `${process.env.REACT_APP_AUTHORITY}/account/login`));
        }
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
