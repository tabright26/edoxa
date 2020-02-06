import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalHeader } from "reactstrap";
import { CREATE_USER_TRANSACTION_MODAL } from "utils/modal/constants";
import UserTransactionForm from "components/Transaction/Form";
import { compose } from "recompose";
import { CurrencyType, TransactionType } from "types";
import { ModalSubtitle } from "components/Shared/Modal/Subtitle";

type OutterProps = {};

type InnerProps = InjectedProps & {
  currency: CurrencyType;
  transactionType: TransactionType;
  title: string;
  description: string;
};

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({
  show,
  handleHide,
  currency,
  transactionType,
  title,
  description
}) => {
  return (
    <Modal backdrop="static" centered isOpen={show} toggle={handleHide}>
      <ModalHeader toggle={handleHide} className="my-auto bg-gray-900">
        <span className="d-block text-uppercase">{title}</span>
        {description && <ModalSubtitle>{description}</ModalSubtitle>}
      </ModalHeader>
      {show && (
        <UserTransactionForm.Create
          transactionType={transactionType}
          currency={currency}
          handleCancel={handleHide}
        />
      )}
    </Modal>
  );
};

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: CREATE_USER_TRANSACTION_MODAL, destroyOnHide: false })
);

export default enhance(Create);
