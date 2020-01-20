import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_USER_ADDRESS_MODAL } from "utils/modal/constants";
import AddressForm from "components/User/Address/Form";
import { compose } from "recompose";
import { sentenceCase } from "change-case";
import { connect, DispatchProp } from "react-redux";
import {  destroy } from "redux-form";
import { CREATE_USER_ADDRESS_FORM } from "utils/form/constants";

type InnerProps = DispatchProp & InjectedProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const CustomModal: FunctionComponent<Props> = ({
  show,
  handleHide,
  dispatch
}) => (
  <Modal
    unmountOnClose={false}
    backdrop="static"
    centered
    isOpen={show}
    toggle={handleHide}
    onClosed={() => dispatch(destroy(CREATE_USER_ADDRESS_FORM))}
  >
    <ModalHeader toggle={handleHide}>
      {sentenceCase("Add new address")}
    </ModalHeader>
    <ModalBody>
      <AddressForm.Create handleCancel={handleHide} />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connect(),
  connectModal({ name: CREATE_USER_ADDRESS_MODAL, destroyOnHide: false })
);

export default enhance(CustomModal);
