import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal, InjectedProps } from "redux-modal";
import StripePaymentMethodForm from "components/Payment/Stripe/PaymentMethod/Form";
import { compose } from "recompose";
import { StripePaymentMethod } from "types";
import { connect, DispatchProp } from "react-redux";
import { destroy } from "redux-form";
import { DELETE_STRIPE_PAYMENTMETHOD_FORM } from "utils/form/constants";
import { DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";

type InnerProps = DispatchProp &
  InjectedProps & {
    paymentMethod: StripePaymentMethod;
  };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Delete: FunctionComponent<Props> = ({
  show,
  handleHide,
  paymentMethod,
  dispatch
}) => (
  <Modal
    backdrop="static"
    centered
    isOpen={show}
    toggle={handleHide}
    onClosed={() => dispatch(destroy(DELETE_STRIPE_PAYMENTMETHOD_FORM))}
  >
    <ModalHeader
      className="text-uppercase my-auto bg-gray-900"
      toggle={handleHide}
    >
      DELETE PAYMENT METHOD
    </ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Delete
        paymentMethodId={paymentMethod.id}
        handleCancel={() => handleHide()}
      />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connect(),
  connectModal({
    name: DELETE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(Delete);
