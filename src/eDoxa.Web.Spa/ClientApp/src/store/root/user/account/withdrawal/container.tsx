import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadWithdrawalAmounts, withdrawal } from "./actions";
import { RootState } from "store/root/types";
import { Currency } from "types";

export const withUserAccountWithdrawal = (currency: Currency) => (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.actions.loadWithdrawalAmounts();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      amounts: state.user.account.withdrawal.data.amounts.get(currency)
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadWithdrawalAmounts: () => dispatch(loadWithdrawalAmounts(currency)),
        withdrawal: (amount: number) => dispatch(withdrawal(currency, amount))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
