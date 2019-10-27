import React, { Fragment } from "react";
import { STRIPE_CARD_TYPE } from "types";
import { Elements } from "react-stripe-elements";

const Cards = React.lazy(() => import("./Cards"));
const BankAccount = React.lazy(() => import("./BankAccount"));

const PaymentMethods = () => (
  <Fragment>
    <h5 className="text-uppercase">PAYMENT METHODS</h5>
    <Elements>
      <BankAccount className="my-4" />
    </Elements>
    <Cards className="my-4" paymentMethodType={STRIPE_CARD_TYPE} />
  </Fragment>
);

export default PaymentMethods;