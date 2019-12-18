import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadTransactionBundles } from "store/actions/cashier";
import { RootState } from "store/types";
import { TRANSACTION_TYPE_WITHDRAWAL } from "types";

export const withUserAccountWithdrawalBundles = (
  HighOrderComponent: FunctionComponent<any>
) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      if (!props.bundles.data.length) {
        props.loadUserAccountWithdrawalBundles();
      }
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState, ownProps: any) => {
    return {
      bundles: state.root.user.account.withdrawal.bundles[ownProps.currency]
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: any) => {
    return {
      loadUserAccountWithdrawalBundles: () =>
        dispatch(
          loadTransactionBundles(TRANSACTION_TYPE_WITHDRAWAL, ownProps.currency)
        )
    };
  };

  return connect(mapStateToProps, mapDispatchToProps)(Container);
};
