import React, { Component } from "react";
import { connect } from "react-redux";
import { push } from "connected-react-router";
import { forgotPassword, resetPassword, confirmEmail } from "../../../store/actions/identityActions";

const withUserContainer = WrappedComponent => {
  class UserContainer extends Component {
    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

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
        confirmEmail: (userId, code) => dispatch(confirmEmail(userId, code)),
        forgotPassword: fields => dispatch(forgotPassword(fields)).then(dispatch(push("/"))),
        resetPassword: (fields, code) => {
          const data = fields;
          delete data.confirmPassword;
          data.code = code;
          return dispatch(resetPassword(data)).then((window.location.href = `${process.env.REACT_APP_AUTHORITY}/account/login`));
        }
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserContainer);
};

export default withUserContainer;
