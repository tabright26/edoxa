import React, { Fragment } from "react";
import MoneyTransactionHistory from "./Money";
import TokenTransactionHistory from "./Token";

const TransactionHistory = () => (
  <Fragment>
    <h5 className="mb-4">TRANSACTION HISTORY</h5>
    <MoneyTransactionHistory />
    <TokenTransactionHistory />
  </Fragment>
);

export default TransactionHistory;
