import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { push } from "connected-react-router";
import { confirmEmail } from "reducers/user/email/actions";
import { forgotPassword, resetPassword } from "reducers/user/password/actions";

const connectUser = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, user, balance, isAuthenticated, ...attributes }) => (
    <ConnectedComponent actions={actions} user={user} balance={balance} isAuthenticated={isAuthenticated} {...attributes} />
  );

  const mapStateToProps = state => {
    const user = state.oidc.user;
    return {
      user: user,
      balance: state.user.account.balance,
      isAuthenticated: user
    };
  };

  const mapDispatchToProps = dispatch => {
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

export default connectUser;
