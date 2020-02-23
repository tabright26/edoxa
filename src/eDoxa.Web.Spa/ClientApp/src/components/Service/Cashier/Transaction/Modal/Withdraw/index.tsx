import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalHeader } from "reactstrap";
import { WITHDRAW_TRANSACTION_MODAL } from "utils/modal/constants";
import TransactionForm from "components/Service/Cashier/Transaction/Form";
import { compose } from "recompose";
import { CurrencyType } from "types/cashier";
import { ModalSubtitle } from "components/Shared/Modal/Subtitle";

type OutterProps = {};

type InnerProps = InjectedProps & {
  currencyType: CurrencyType;
  title: string;
  description: string;
};

type Props = InnerProps & OutterProps;

const Withdraw: FunctionComponent<Props> = ({
  show,
  handleHide,
  currencyType,
  title,
  description
}) => (
  <Modal backdrop="static" centered isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide} className="my-auto">
      <span className="d-block text-uppercase">{title}</span>
      {description && <ModalSubtitle>{description}</ModalSubtitle>}
    </ModalHeader>
    {show && (
      <TransactionForm.Withdraw
        currencyType={currencyType}
        handleCancel={handleHide}
      />
    )}
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: WITHDRAW_TRANSACTION_MODAL, destroyOnHide: false })
);

export default enhance(Withdraw);
