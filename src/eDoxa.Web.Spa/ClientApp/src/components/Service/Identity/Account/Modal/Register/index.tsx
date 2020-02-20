import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import AccountForm from "components/Service/Identity/Account/Form";
import { compose } from "recompose";
import { REGISTER_ACCOUNT_MODAL } from "utils/modal/constants";
import { ModalSubtitle } from "components/Shared/Modal/Subtitle";

type InnerProps = InjectedProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Register: FunctionComponent<Props> = ({ show, handleHide }) => (
  <Modal backdrop="static" centered isOpen={show} toggle={handleHide}>
    <ModalHeader className="bg-gray-900" toggle={handleHide}>
      <strong className="text-uppercase">REGISTER</strong>
      <ModalSubtitle>Create your account</ModalSubtitle>
    </ModalHeader>
    <ModalBody>{show && <AccountForm.Register />}</ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: REGISTER_ACCOUNT_MODAL, destroyOnHide: false })
);

export default enhance(Register);
