import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadWithdrawalAmounts, withdrawal } from "./actions";
import { AppState } from "store/types";
import { Currency } from "../types";

export const connectUserAccountDeposit = (currency: Currency) => (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, amounts, ...attributes }) => {
    useEffect((): void => {
      actions.loadWithdrawalAmounts();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} amounts={amounts} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    switch (currency) {
      case "token":
        return {
          amounts: state.user.account.withdrawal.amounts.get("token")
        };
      default:
        throw new Error("Invalid deposit currency.");
    }
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
