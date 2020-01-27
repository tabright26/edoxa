import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody, Button } from "reactstrap";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { withStripePaymentMethods } from "store/root/payment/stripe/paymentMethod/container";
import { StripePaymentMethod } from "types";
import PaymentMethodCard from "components/Payment/Stripe/PaymentMethod/Card";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { connect, MapDispatchToProps } from "react-redux";
import { show } from "redux-modal";
import {
  CREATE_STRIPE_PAYMENTMETHOD_MODAL,
  UPDATE_STRIPE_PAYMENTMETHOD_MODAL,
  DELETE_STRIPE_PAYMENTMETHOD_MODAL
} from "utils/modal/constants";
import { RootState } from "store/types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

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
          <PaymentMethodCard.Icon
            className="my-auto"
            brand={paymentMethod.card.brand}
            size="2x"
          />
          <span className="ml-2 my-auto">{`XXXX XXXX XXXX ${paymentMethod.card.last4}`}</span>
        </dd>
        <dd className="col-sm-2 m-0 d-flex">
          <span className="my-auto mr-2 text-muted">Expiration:</span>
          <PaymentMethodCard.Exp
            className="my-auto"
            month={paymentMethod.card.expMonth}
            year={paymentMethod.card.expYear}
          />
        </dd>
        <dd className="col-sm-6 mb-0 d-flex">
          <Button
            className="p-0 ml-auto my-auto"
            color="link"
            size="sm"
            onClick={() => showDeleteStripePaymentMethodModal(paymentMethod)}
          >
            <small className="text-uppercase">
              <FontAwesomeIcon icon={faTimes} /> REMOVE
            </small>
          </Button>
          <Button
            className="p-0 ml-auto my-auto"
            color="link"
            size="sm"
            onClick={() => showUpdateStripePaymentMethodModal(paymentMethod)}
          >
            <small className="text-uppercase">
              <FontAwesomeIcon icon={faEdit} /> UPDATE
            </small>
          </Button>
        </dd>
      </dl>
      {hasMore && <hr className="border-secondary" />}
    </>
  );
};

const Panel: FunctionComponent<any> = ({
  className,
  limit,
  showCreateStripePaymentMethodModal,
  showUpdateStripePaymentMethodModal,
  showDeleteStripePaymentMethodModal,
  paymentMethods: { data, loading, error }
}) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">CARDS</strong>
      <small className="ml-2 my-auto text-muted">
        ({data.length}/{limit})
      </small>
      <Button
        className="p-0 ml-auto my-auto"
        color="link"
        size="sm"
        onClick={() => showCreateStripePaymentMethodModal()}
        disabled={data.length >= limit}
      >
        <small className="text-uppercase">
          <FontAwesomeIcon icon={faPlus} /> ADD A NEW CARD
        </small>
      </Button>
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

const mapStateToProps = (state: RootState) => {
  return {
    limit: state.static.payment.stripe.paymentMethod.card.limit
  };
};

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
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(Panel);
