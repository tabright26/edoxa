import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { RootState } from "store/types";

export const connectStripeConnectAccount = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, accountAccount, ...attributes }) => <ConnectedComponent actions={actions} accountAccount={accountAccount} {...attributes} />;

  const mapStateToProps = (state: RootState) => {
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
