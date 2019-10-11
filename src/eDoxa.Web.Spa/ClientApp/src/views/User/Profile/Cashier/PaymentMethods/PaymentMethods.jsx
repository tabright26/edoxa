import React, { Fragment, Suspense } from "react";
import { Button } from "reactstrap";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Loading from "components/Shared/Loading";
import { connectStripePaymentMethods } from "store/stripe/paymentMethods/container";
import { CARD_PAYMENTMETHOD_TYPE } from "store/stripe/paymentMethods/types";

import StripePaymentMethodModal from "modals/Stripe/PaymentMethod";
import { Elements } from "react-stripe-elements";

const Cards = React.lazy(() => import("./Cards"));

const PaymentMethods = ({ actions }) => (
  <Fragment>
    <h5>
      PAYMENT METHODS
      <Button className="float-right" size="sm" color="link" onClick={() => actions.showCreatePaymentMethodModal()}>
        <FontAwesomeIcon icon={faPlus} /> ADD A NEW PAYMENT METHOD
      </Button>
    </h5>
    <Elements>
      <StripePaymentMethodModal.Create actions={actions} />
    </Elements>
    <StripePaymentMethodModal.Update />
    <StripePaymentMethodModal.Delete />
    <Suspense fallback={<Loading.Default />}>
      <Cards className="card-accent-primary my-4" />
    </Suspense>
  </Fragment>
);

export default connectStripePaymentMethods(CARD_PAYMENTMETHOD_TYPE)(PaymentMethods);
