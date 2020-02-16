import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal, InjectedProps } from "redux-modal";
import StripePaymentMethodForm from "components/Service/Payment/Stripe/PaymentMethod/Form";
import { compose } from "recompose";
import { DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";
import { StripePaymentMethod } from "types/payment";

type InnerProps = InjectedProps & {
  paymentMethod: StripePaymentMethod;
};

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Delete: FunctionComponent<Props> = ({
  show,
  handleHide,
  paymentMethod
}) => (
  <Modal backdrop="static" centered isOpen={show} toggle={handleHide}>
    <ModalHeader
      className="text-uppercase my-auto bg-gray-900"
      toggle={handleHide}
    >
      DELETE CREDIT CARD
    </ModalHeader>
    <ModalBody>
      {show && (
        <StripePaymentMethodForm.Delete
          paymentMethodId={paymentMethod.id}
          handleCancel={() => handleHide()}
        />
      )}
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({
    name: DELETE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(Delete);
