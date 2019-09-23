import React, { Component, FunctionComponent } from "react";
import { connect } from "react-redux";

const connectStripeConnectAccount = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, accountAccount, ...attributes }) => <ConnectedComponent actions={actions} accountAccount={accountAccount} {...attributes} />;

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
