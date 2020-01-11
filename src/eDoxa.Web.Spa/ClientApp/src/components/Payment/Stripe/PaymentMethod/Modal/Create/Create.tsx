import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal, InjectedProps } from "redux-modal";
import StripePaymentMethodForm from "components/Payment/Stripe/PaymentMethod/Form";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";
import { compose } from "recompose";

type InnerProps = InjectedProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const CustomModal: FunctionComponent<Props> = ({
  show,
  handleHide
}) => (
  <Modal className="modal-dialog-centered" isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>ADD NEW PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Create handleCancel={() => handleHide()} />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({
    name: CREATE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(CustomModal);
