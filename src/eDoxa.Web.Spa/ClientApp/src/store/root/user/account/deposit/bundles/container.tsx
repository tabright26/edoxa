import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadUserAccountDepositBundlesFor } from "store/actions/cashier";

export const withUserAccountDepositBundles = (
  HighOrderComponent: FunctionComponent<any>
) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      if (!props.bundles.data.length) {
        props.loadUserAccountDepositBundles();
      }
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState, ownProps: any) => {
    return {
      bundles: state.root.user.account.deposit.bundles[ownProps.currency]
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: any) => {
    return {
      loadUserAccountDepositBundles: () =>
        dispatch(loadUserAccountDepositBundlesFor(ownProps.currency))
    };
  };

  return connect(mapStateToProps, mapDispatchToProps)(Container);
};
