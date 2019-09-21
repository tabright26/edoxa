import React, { Fragment } from "react";
import DepositTransactions from "./Money";
import WithdrawalTransactions from "./Token";

const PaymentMethods = ({ actions }) => (
  <Fragment>
    <h5>TRANSACTION HISTORY</h5>
    <DepositTransactions className="card-accent-primary my-4" />
    <WithdrawalTransactions />
  </Fragment>
);

export default PaymentMethods;
