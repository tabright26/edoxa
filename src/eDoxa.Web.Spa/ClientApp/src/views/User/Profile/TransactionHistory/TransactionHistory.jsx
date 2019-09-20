import React, { Fragment } from "react";
import Transactions from "./Transactions";

const PaymentMethods = ({ actions }) => (
  <Fragment>
    <h5 className="my-4">TRANSACTION HISTORY</h5>
    <Transactions className="card-accent-primary my-4" />
  </Fragment>
);

export default PaymentMethods;
