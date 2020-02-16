import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import PaymentMethodCard from "components/Service/Payment/Stripe/PaymentMethod/Card";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import { show } from "redux-modal";
import {
  CREATE_STRIPE_PAYMENTMETHOD_MODAL,
  UPDATE_STRIPE_PAYMENTMETHOD_MODAL,
  DELETE_STRIPE_PAYMENTMETHOD_MODAL
} from "utils/modal/constants";
import Button from "components/Shared/Button";
import { RootState } from "store/types";
import { StripePaymentMethod } from "types/payment";

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
          <Button.Link
            className="p-0 ml-auto my-auto"
            icon={faTimes}
            size="sm"
            uppercase
            onClick={() => showDeleteStripePaymentMethodModal(paymentMethod)}
          >
            REMOVE
          </Button.Link>
          <Button.Link
            className="p-0 ml-auto my-auto"
            icon={faEdit}
            size="sm"
            uppercase
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
      <strong className="text-uppercase my-auto">CREDIT CARDS</strong>
      <small className="ml-2 my-auto text-muted">
        ({data.length}/{limit})
      </small>
      <Button.Link
        className="p-0 ml-auto my-auto"
        icon={faPlus}
        size="sm"
        uppercase
        onClick={() => showCreateStripePaymentMethodModal()}
        disabled={data.length >= limit}
      >
        ADD A NEW CREDIT CARD
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

const mapStateToProps: MapStateToProps<any, any, RootState> = state => {
  return {
    limit: 5,
    paymentMethods: state.root.payment.stripe.paymentMethods
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

const enhance = compose<any, any>(connect(mapStateToProps, mapDispatchToProps));

export default enhance(Panel);
