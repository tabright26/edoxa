import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { DEPOSIT_MODAL } from "modals";
import { loadDepositAmounts, deposit } from "./actions";
import { AppState } from "store/types";
import { Currency } from "../types";

export const connectUserAccountDeposit = (currency: Currency) => (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, amounts, ...attributes }) => {
    useEffect((): void => {
      actions.loadDepositAmounts();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} amounts={amounts} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      amounts: state.user.account.deposit.amounts.get(currency)
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadDepositAmounts: () => dispatch(loadDepositAmounts(currency)),
        deposit: (amount: number) => dispatch(deposit(currency, amount)),
        showDepositModal: (actions, amounts) => dispatch(show(DEPOSIT_MODAL, { actions, amounts }))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};