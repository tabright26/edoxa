import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal, InjectedProps } from "redux-modal";
import StripePaymentMethodForm from "components/Payment/Stripe/PaymentMethod/Form";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";
import { compose } from "recompose";
import { connect, DispatchProp } from "react-redux";
import { destroy } from "redux-form";
import { CREATE_STRIPE_PAYMENTMETHOD_FORM } from "utils/form/constants";
import { injectStripe, ReactStripeElements } from "react-stripe-elements";

type InnerProps = DispatchProp &
  InjectedProps &
  ReactStripeElements.InjectedStripeProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({
  show,
  handleHide,
  dispatch,
  elements
}) => (
  <Modal
    backdrop="static"
    centered
    isOpen={show}
    toggle={handleHide}
    onClosed={() => {
      dispatch(destroy(CREATE_STRIPE_PAYMENTMETHOD_FORM));
      elements.getElement("cardNumber").clear();
      elements.getElement("cardExpiry").clear();
      elements.getElement("cardCvc").clear();
    }}
  >
    <ModalHeader
      className="text-uppercase my-auto bg-gray-900"
      toggle={handleHide}
    >
      ADD NEW PAYMENT METHOD
    </ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Create handleCancel={() => handleHide()} />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  injectStripe,
  connect(),
  connectModal({
    name: CREATE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(Create);
