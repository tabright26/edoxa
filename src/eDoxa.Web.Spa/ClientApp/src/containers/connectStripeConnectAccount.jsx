import React, { Component } from "react";
import { connect } from "react-redux";

const connectStripeConnectAccount = WrappedComponent => {
  class Container extends Component {
    render() {
      return <WrappedComponent accountAccount={this.props.accountAccount} />;
    }
  }

  const mapStateToProps = state => {
    return {
      accountAccount: {}
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {}
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectStripeConnectAccount;
