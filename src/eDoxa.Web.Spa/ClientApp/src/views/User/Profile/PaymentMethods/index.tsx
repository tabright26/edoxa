import React, { Fragment } from "react";
import { Elements } from "react-stripe-elements";

const Cards = React.lazy(() => import("./Cards"));
const BankAccount = React.lazy(() => import("./BankAccount"));

const PaymentMethods = () => (
  <Fragment>
    <h5 className="text-uppercase my-4">PAYMENT METHODS</h5>
    <Elements>
      <BankAccount className="my-4" />
    </Elements>
    <Cards className="my-4" />
  </Fragment>
);

export default PaymentMethods;
