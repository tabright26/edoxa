import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal, InjectedProps } from "redux-modal";
import StripePaymentMethodForm from "components/Payment/Stripe/PaymentMethod/Form";
import { UPDATE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";
import { compose } from "recompose";
import { StripePaymentMethod } from "types";
import { connect, DispatchProp } from "react-redux";
import { destroy } from "redux-form";
import { UPDATE_STRIPE_PAYMENTMETHOD_FORM } from "utils/form/constants";

type InnerProps = DispatchProp &
  InjectedProps & {
    paymentMethod: StripePaymentMethod;
  };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Update: FunctionComponent<Props> = ({
  show,
  handleHide,
  paymentMethod,
  dispatch
}) => (
  <Modal
    unmountOnClose={false}
    backdrop="static"
    centered
    size="lg"
    isOpen={show}
    toggle={handleHide}
    onClosed={() => dispatch(destroy(UPDATE_STRIPE_PAYMENTMETHOD_FORM))}
  >
    <ModalHeader
      className="text-uppercase my-auto bg-gray-900"
      toggle={handleHide}
    >
      UPDATE PAYMENT METHOD
    </ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Update
        paymentMethodId={paymentMethod.id}
        handleCancel={() => handleHide()}
      />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connect(),
  connectModal({
    name: UPDATE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(Update);
