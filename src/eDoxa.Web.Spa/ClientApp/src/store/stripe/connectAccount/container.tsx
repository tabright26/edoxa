import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { AppState } from "store/types";

const connectStripeConnectAccount = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, accountAccount, ...attributes }) => <ConnectedComponent actions={actions} accountAccount={accountAccount} {...attributes} />;

  const mapStateToProps = (state: AppState) => {
    return {
      accountAccount: {}
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
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
