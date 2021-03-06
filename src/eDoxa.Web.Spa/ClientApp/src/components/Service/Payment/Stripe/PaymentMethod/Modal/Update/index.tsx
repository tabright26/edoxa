import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal, InjectedProps } from "redux-modal";
import StripePaymentMethodForm from "components/Service/Payment/Stripe/PaymentMethod/Form";
import { UPDATE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";
import { compose } from "recompose";
import { StripePaymentMethod } from "types/payment";

type InnerProps = InjectedProps & {
  paymentMethod: StripePaymentMethod;
};

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Update: FunctionComponent<Props> = ({
  show,
  handleHide,
  paymentMethod
}) => (
  <Modal backdrop="static" centered size="lg" isOpen={show} toggle={handleHide}>
    <ModalHeader className="text-uppercase my-auto" toggle={handleHide}>
      UPDATE CREDIT CARD
    </ModalHeader>
    <ModalBody>
      {show && (
        <StripePaymentMethodForm.Update
          paymentMethodId={paymentMethod.id}
          handleCancel={() => handleHide()}
        />
      )}
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({
    name: UPDATE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(Update);
