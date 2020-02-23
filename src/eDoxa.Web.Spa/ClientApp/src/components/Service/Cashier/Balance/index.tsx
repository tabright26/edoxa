import React, { FunctionComponent } from "react";
import Format from "components/Shared/Format";
import { compose } from "recompose";
import { CurrencyType, TransactionStatus, Currency } from "types/cashier";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";

type OwnProps = {
  currencyType: CurrencyType;
  transactionStatus: TransactionStatus;
  alignment?: "right" | "left" | "center" | "justify";
};

type StateProps = {
  currency: Currency;
};

type InnerProps = StateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Balance: FunctionComponent<Props> = ({
  currency,
  alignment = "justify"
}) => <Format.Currency currency={currency} alignment={alignment} />;

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  return {
    currency: {
      type: ownProps.currencyType,
      amount: state.root.user.transactionHistory.data
        .filter(
          transaction =>
            transaction.status.toUpperCase() ===
              ownProps.transactionStatus.toUpperCase() &&
            transaction.currency.type.toUpperCase() ===
              ownProps.currencyType.toUpperCase()
        )
        .map(transaction => transaction.currency.amount)
        .reduce((total, amount) => total + amount, 0)
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(Balance);
