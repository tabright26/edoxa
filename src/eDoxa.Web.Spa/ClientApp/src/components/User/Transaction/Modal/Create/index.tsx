import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_USER_TRANSACTION_MODAL } from "utils/modal/constants";
import UserTransactionForm from "components/User/Transaction/Form";
import { compose } from "recompose";
import { Currency, TransactionType } from "types";
import { connect, DispatchProp } from "react-redux";
import { destroy } from "redux-form";
import { CREATE_USER_TRANSACTION_FORM } from "utils/form/constants";

type OutterProps = {};

type InnerProps = InjectedProps &
  DispatchProp & {
    currency: Currency;
    transactionType: TransactionType;
    title: string;
  };

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({
  show,
  handleHide,
  currency,
  transactionType,
  title,
  dispatch
}) => (
  <Modal
    unmountOnClose={false}
    backdrop="static"
    size="lg"
    centered
    isOpen={show}
    toggle={handleHide}
    onClosed={() => dispatch(destroy(CREATE_USER_TRANSACTION_FORM))}
  >
    <ModalHeader
      toggle={handleHide}
      className="text-uppercase my-auto bg-gray-900"
    >
      {title}
    </ModalHeader>
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
  connect(),
  connectModal({ name: CREATE_USER_TRANSACTION_MODAL, destroyOnHide: false })
);

export default enhance(Create);
