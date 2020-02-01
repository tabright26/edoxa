import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal, InjectedProps } from "redux-modal";
import StripePaymentMethodForm from "components/Payment/Stripe/PaymentMethod/Form";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";
import { compose } from "recompose";
import { ReactStripeElements, Elements } from "react-stripe-elements";
import { ModalSubtitle } from "components/Shared/Modal/Subtitle";

type InnerProps = InjectedProps & ReactStripeElements.InjectedStripeProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({ show, handleHide }) => (
  <Modal backdrop="static" centered isOpen={show} toggle={handleHide}>
    <ModalHeader className="my-auto bg-gray-900" toggle={handleHide}>
      <span className="d-block text-uppercase">ADD NEW PAYMENT METHOD</span>
      <ModalSubtitle>We accept all credit card</ModalSubtitle>
    </ModalHeader>
    {show && (
      <ModalBody>
        <Elements>
          <StripePaymentMethodForm.Create handleCancel={() => handleHide()} />
        </Elements>
      </ModalBody>
    )}
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({
    name: CREATE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(Create);
