import React, { Component } from "react";
import { connect } from "react-redux";
import { resetPassword } from "../../../store/actions/identityActions";

const withResetPassword = WrappedComponent => {
  class ResetPasswordContainer extends Component {
    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        resetPassword: (fields, code) => {
          const data = fields;
          delete data.confirmPassword;
          data.code = code;
          console.log(data);
          return dispatch(resetPassword(data));
        }
      }
    };
  };

  return connect(
    null,
    mapDispatchToProps
  )(ResetPasswordContainer);
};

export default withResetPassword;
