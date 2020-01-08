import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { compose } from "recompose";
import { Currency, TransactionType } from "types";
import { connect, MapDispatchToProps } from "react-redux";
import { show } from "redux-modal";
import { CREATE_TRANSACTION_MODAL } from "modals";

interface DispatchProps {
  showModal: () => void;
}

interface OwnProps {
  currency: Currency;
  transactionType: TransactionType;
}

type InnerProps = DispatchProps;

type OutterProps = OwnProps & {
  disabled?: boolean;
};

type Props = InnerProps & OutterProps;

const CreateTransactionButton: FunctionComponent<Props> = ({
  showModal,
  disabled = false,
  children
}) => (
  <Button
    color="primary"
    size="sm"
    disabled={disabled}
    block
    onClick={() => showModal()}
  >
    {children}
  </Button>
);

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch,
  ownProps
) => {
  return {
    showModal: () =>
      dispatch(
        show(CREATE_TRANSACTION_MODAL, {
          currency: ownProps.currency,
          transactionType: ownProps.transactionType
        })
      )
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(null, mapDispatchToProps)
);

export default enhance(CreateTransactionButton);
