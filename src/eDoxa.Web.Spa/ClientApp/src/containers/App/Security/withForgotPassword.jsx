import React, { Component } from "react";
import { connect } from "react-redux";
import { forgotPassword } from "../../../store/actions/identityActions";

const withForgotPassword = WrappedComponent => {
  class ForgotPasswordContainer extends Component {
    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        forgotPassword: fields => dispatch(forgotPassword(fields))
      }
    };
  };

  return connect(
    null,
    mapDispatchToProps
  )(ForgotPasswordContainer);
};

export default withForgotPassword;
