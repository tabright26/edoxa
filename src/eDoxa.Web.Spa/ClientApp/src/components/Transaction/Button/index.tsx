import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { compose } from "recompose";
import {
  CurrencyType,
  TransactionType,
  TRANSACTION_TYPE_DEPOSIT,
  TRANSACTION_TYPE_WITHDRAWAL
} from "types/cashier";
import { connect, MapDispatchToProps } from "react-redux";
import { show } from "redux-modal";
import {
  DEPOSIT_TRANSACTION_MODAL,
  WITHDRAW_TRANSACTION_MODAL
} from "utils/modal/constants";

interface DispatchProps {
  showModal: () => void;
}

interface OwnProps {
  disabled?: boolean;
  currencyType: CurrencyType;
  transactionType: TransactionType;
  title: string;
  description: string;
}

type InnerProps = DispatchProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({
  showModal,
  children,
  disabled
}) => (
  <Button
    color="primary"
    size="sm"
    block
    onClick={() => showModal()}
    disabled={disabled}
  >
    {children}
  </Button>
);

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch,
  ownProps
) => {
  return {
    showModal: () => {
      if (ownProps.transactionType === TRANSACTION_TYPE_DEPOSIT) {
        dispatch(
          show(DEPOSIT_TRANSACTION_MODAL, {
            currencyType: ownProps.currencyType,
            title: ownProps.title,
            description: ownProps.description
          })
        );
      }
      if (ownProps.transactionType === TRANSACTION_TYPE_WITHDRAWAL) {
        dispatch(
          show(WITHDRAW_TRANSACTION_MODAL, {
            currencyType: ownProps.currencyType,
            title: ownProps.title,
            description: ownProps.description
          })
        );
      }
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(null, mapDispatchToProps)
);

export default enhance(Create);
