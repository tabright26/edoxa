import React, { Fragment } from "react";
import FilteredTransactions from "./FilteredTransactions";

const TransactionHistory = () => (
  <Fragment>
    <h5 className="text-uppercase mb-4">TRANSACTION HISTORY</h5>
    <FilteredTransactions currency={null} type={null} status={null} />
  </Fragment>
);

export default TransactionHistory;
