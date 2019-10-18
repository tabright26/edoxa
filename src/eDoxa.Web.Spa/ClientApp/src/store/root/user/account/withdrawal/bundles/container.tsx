import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserAccountWithdrawalBundlesFor } from "./actions";
import { RootState } from "store/root/types";

export const withUserAccountWithdrawalBundles = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserAccountWithdrawalBundles();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState, ownProps: any) => {
    return {
      bundles: state.user.account.withdrawal.bundles[ownProps.currency]
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: any) => {
    return {
      loadUserAccountWithdrawalBundles: () => dispatch(loadUserAccountWithdrawalBundlesFor(ownProps.currency))
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
