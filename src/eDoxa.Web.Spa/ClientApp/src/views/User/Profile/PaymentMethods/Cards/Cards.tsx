import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { withStripePaymentMethods } from "store/root/payment/stripe/paymentMethod/container";
import { StripePaymentMethod } from "types";
import CardBrandIcon from "components/Payment/Stripe/PaymentMethod/Card/Icon";
import CardExpiration from "components/Payment/Stripe/PaymentMethod/Card/Expiration";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import Button from "components/Shared/Button";
import { compose } from "recompose";
import Loading from "components/Shared/Loading";
import { connect, MapDispatchToProps } from "react-redux";
import { show } from "redux-modal";
import {
  CREATE_STRIPE_PAYMENTMETHOD_MODAL,
  UPDATE_STRIPE_PAYMENTMETHOD_MODAL,
  DELETE_STRIPE_PAYMENTMETHOD_MODAL
} from "utils/modal/constants";

interface OwnProps {}

interface DispatchProps {
  showCreateStripePaymentMethodModal: () => void;
  showUpdateStripePaymentMethodModal: (
    paymentMethod: StripePaymentMethod
  ) => void;
  showDeleteStripePaymentMethodModal: (
    paymentMethod: StripePaymentMethod
  ) => void;
}

const StripeCardItem: FunctionComponent<any> = ({
  hasMore,
  showUpdateStripePaymentMethodModal,
  showDeleteStripePaymentMethodModal,
  paymentMethod
}) => {
  return (
    <>
      <dl className={`row ${!hasMore && "mb-0"}`}>
        <dd className="col-sm-4 m-0 d-flex">
          <CardBrandIcon
            className="my-auto"
            brand={paymentMethod.card.brand}
            size="2x"
          />
          <span className="ml-2 my-auto">{`XXXX XXXX XXXX ${paymentMethod.card.last4}`}</span>
        </dd>
        <dd className="col-sm-2 m-0 d-flex">
          <span className="my-auto mr-2 text-muted">Expiration:</span>
          <CardExpiration
            className="my-auto"
            month={paymentMethod.card.expMonth}
            year={paymentMethod.card.expYear}
          />
        </dd>
        <dd className="col-sm-6 mb-0 d-flex">
          <Button.Link
            className="p-0 ml-auto my-auto"
            icon={faTimes}
            onClick={() => showDeleteStripePaymentMethodModal(paymentMethod)}
          >
            REMOVE
          </Button.Link>
          <Button.Link
            className="p-0 ml-auto my-auto"
            icon={faEdit}
            onClick={() => showUpdateStripePaymentMethodModal(paymentMethod)}
          >
            UPDATE
          </Button.Link>
        </dd>
      </dl>
      {hasMore && <hr className="border-secondary" />}
    </>
  );
};

const StripeCards: FunctionComponent<any> = ({
  className,
  showCreateStripePaymentMethodModal,
  showUpdateStripePaymentMethodModal,
  showDeleteStripePaymentMethodModal,
  paymentMethods: { data, loading, error }
}) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">CARDS</strong>
      <Button.Link
        className="p-0 ml-auto my-auto"
        icon={faPlus}
        onClick={() => showCreateStripePaymentMethodModal()}
      >
        ADD A NEW CARD
      </Button.Link>
    </CardHeader>
    <CardBody>
      {loading ? (
        <Loading />
      ) : (
        data.map((paymentMethod, index) => (
          <StripeCardItem
            key={index}
            paymentMethod={paymentMethod}
            hasMore={data.length !== index + 1}
            showUpdateStripePaymentMethodModal={
              showUpdateStripePaymentMethodModal
            }
            showDeleteStripePaymentMethodModal={
              showDeleteStripePaymentMethodModal
            }
          />
        ))
      )}
    </CardBody>
  </Card>
);

const mapDispatchToProps: MapDispatchToProps<
  DispatchProps,
  OwnProps
> = dispatch => {
  return {
    showCreateStripePaymentMethodModal: () =>
      dispatch(show(CREATE_STRIPE_PAYMENTMETHOD_MODAL)),
    showUpdateStripePaymentMethodModal: (paymentMethod: StripePaymentMethod) =>
      dispatch(show(UPDATE_STRIPE_PAYMENTMETHOD_MODAL, { paymentMethod })),
    showDeleteStripePaymentMethodModal: (paymentMethod: StripePaymentMethod) =>
      dispatch(show(DELETE_STRIPE_PAYMENTMETHOD_MODAL, { paymentMethod }))
  };
};

const enhance = compose<any, any>(
  withStripePaymentMethods,
  connect(null, mapDispatchToProps)
);

export default enhance(StripeCards);
