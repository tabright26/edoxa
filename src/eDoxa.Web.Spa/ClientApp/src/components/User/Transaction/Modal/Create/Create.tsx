import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_USER_TRANSACTION_MODAL } from "utils/modal/constants";
import UserTransactionForm from "components/User/Transaction/Form";
import { compose } from "recompose";
import { Currency, TransactionType } from "types";

type OutterProps = {};

type InnerProps = InjectedProps & {
  currency: Currency;
  transactionType: TransactionType;
};

type Props = InnerProps & OutterProps;

const CustomModal: FunctionComponent<Props> = ({
  show,
  handleHide,
  currency,
  transactionType
}) => (
  <Modal
    unmountOnClose={false}
    backdrop="static"
    size="lg"
    centered
    isOpen={show}
    toggle={handleHide}
  >
    <ModalHeader toggle={handleHide}>CREATE TRANSACTION</ModalHeader>
    <ModalBody>
      <UserTransactionForm.Create
        transactionType={transactionType}
        currency={currency}
        handleCancel={handleHide}
      />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: CREATE_USER_TRANSACTION_MODAL, destroyOnHide: false })
);

export default enhance(CustomModal);
