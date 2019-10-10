import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { loadWithdrawalAmounts, withdrawal } from "./actions";
import { RootState } from "store/types";
import { Currency } from "../types";
import { WITHDRAWAL_MODAL } from "modals";

export const connectUserAccountWithdrawal = (currency: Currency) => (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, amounts, ...attributes }) => {
    useEffect((): void => {
      actions.loadWithdrawalAmounts();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} amounts={amounts} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      amounts: state.user.account.withdrawal.amounts.get(currency)
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadWithdrawalAmounts: () => dispatch(loadWithdrawalAmounts(currency)),
        withdrawal: (amount: number) => dispatch(withdrawal(currency, amount)),
        showWithdrawalModal: (actions, amounts) => dispatch(show(WITHDRAWAL_MODAL, { actions, amounts }))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
