import React, { Component } from "react";
import { connect } from "react-redux";

const withStripeConnectAccountHoc = WrappedComponent => {
  class UserProfileContainer extends Component {
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
  )(UserProfileContainer);
};

export default withStripeConnectAccountHoc;
