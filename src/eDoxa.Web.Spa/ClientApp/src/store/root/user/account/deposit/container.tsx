import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadDepositAmounts, deposit } from "./actions";
import { RootState } from "store/root/types";
import { Currency } from "types";

export const withUserAccountDeposit = (currency: Currency) => (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, amounts, ...attributes }) => {
    useEffect((): void => {
      actions.loadDepositAmounts();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent actions={actions} amounts={amounts} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      amounts: state.user.account.deposit.data.amounts.get(currency)
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadDepositAmounts: () => dispatch(loadDepositAmounts(currency)),
        deposit: (amount: number) => dispatch(deposit(currency, amount))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
