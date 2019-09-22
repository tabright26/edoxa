import React, { Fragment } from "react";
import DepositTransactionHistory from "./Money";
import WithdrawalTransactionHistory from "./Token";

const TransactionHistory = () => (
  <Fragment>
    <h5 className="mb-4">TRANSACTION HISTORY</h5>
    <DepositTransactionHistory />
    <WithdrawalTransactionHistory />
  </Fragment>
);

export default TransactionHistory;
