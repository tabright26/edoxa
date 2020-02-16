import React, { FunctionComponent } from "react";
import TransactionPanel from "components/Service/Cashier/Transaction/Panel";

const TransactionHistory: FunctionComponent = () => (
  <>
    <h5 className="text-uppercase my-4">TRANSACTION HISTORY</h5>
    <TransactionPanel currency={null} type={null} status={null} />
  </>
);

export default TransactionHistory;
