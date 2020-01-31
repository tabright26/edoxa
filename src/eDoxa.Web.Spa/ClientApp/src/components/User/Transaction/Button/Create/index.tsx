import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { compose } from "recompose";
import { CurrencyType, TransactionType } from "types";
import { connect, MapDispatchToProps } from "react-redux";
import { show } from "redux-modal";
import { CREATE_USER_TRANSACTION_MODAL } from "utils/modal/constants";

interface DispatchProps {
  showModal: () => void;
}

interface OwnProps {
  currency: CurrencyType;
  transactionType: TransactionType;
  title: string;
  description: string;
}

type InnerProps = DispatchProps;

type OutterProps = OwnProps & {
  disabled: boolean;
};

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({
  showModal,
  disabled,
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
        show(CREATE_USER_TRANSACTION_MODAL, {
          currency: ownProps.currency,
          transactionType: ownProps.transactionType,
          title: ownProps.title,
          description: ownProps.description
        })
      )
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(null, mapDispatchToProps)
);

export default enhance(Create);
