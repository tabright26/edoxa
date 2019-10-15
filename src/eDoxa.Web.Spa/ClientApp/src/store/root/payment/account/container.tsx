import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadAccount } from "./actions";

export const withStripeAccount = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, account, ...attributes }) => {
    useEffect((): void => {
      actions.loadAccount();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} accountAccount={account} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      account: state.payment.account.data
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadAccount: () => dispatch(loadAccount())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
