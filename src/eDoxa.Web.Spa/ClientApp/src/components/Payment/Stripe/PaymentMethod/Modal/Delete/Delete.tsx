import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal, InjectedProps } from "redux-modal";
import StripePaymentMethodForm from "components/Payment/Stripe/PaymentMethod/Form";
import { DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";
import { compose } from "recompose";
import { StripePaymentMethod } from "types";

type InnerProps = InjectedProps & {
  paymentMethod: StripePaymentMethod;
};

type OutterProps = {};

type Props = InnerProps & OutterProps;

const CustomModal: FunctionComponent<Props> = ({
  show,
  handleHide,
  paymentMethod
}) => (
  <Modal
    unmountOnClose={false}
    backdrop="static"
    centered
    isOpen={show}
    toggle={handleHide}
  >
    <ModalHeader toggle={handleHide}>DELETE PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Delete
        paymentMethodId={paymentMethod.id}
        handleCancel={() => handleHide()}
      />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({
    name: DELETE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(CustomModal);
