import React, { FunctionComponent } from "react";
import { CURRENCY_TYPE_TOKEN, TRANSACTION_TYPE_DEPOSIT } from "types";
import TransactionButton from "components/Transaction/Button";

const Buy: FunctionComponent = () => (
  <TransactionButton
    transactionType={TRANSACTION_TYPE_DEPOSIT}
    currencyType={CURRENCY_TYPE_TOKEN}
    title="BUY (TOKEN)"
    description={null}
  >
    Buy
  </TransactionButton>
);

export default Buy;
