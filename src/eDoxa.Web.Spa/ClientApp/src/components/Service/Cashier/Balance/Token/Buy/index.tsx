import React, { FunctionComponent } from "react";
import { CURRENCY_TYPE_TOKEN, TRANSACTION_TYPE_DEPOSIT } from "types/cashier";
import TransactionButton from "components/Service/Cashier/Transaction/Button";
import { REACT_APP_BUY_BUTTON_DISABLED } from "keys";

const Buy: FunctionComponent = () => (
  <TransactionButton
    transactionType={TRANSACTION_TYPE_DEPOSIT}
    currencyType={CURRENCY_TYPE_TOKEN}
    title="BUY (TOKEN)"
    description={null}
    disabled={REACT_APP_BUY_BUTTON_DISABLED === "true"}
  >
    Buy
  </TransactionButton>
);

export default Buy;
