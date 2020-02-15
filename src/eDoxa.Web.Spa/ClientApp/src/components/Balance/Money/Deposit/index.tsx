import React, { FunctionComponent } from "react";
import TransactionButton from "components/Transaction/Button";
import { REACT_APP_DEPOSIT_BUTTON_DISABLED } from "keys";
import { TRANSACTION_TYPE_DEPOSIT, CURRENCY_TYPE_MONEY } from "types/cashier";

const Deposit: FunctionComponent = () => (
  <TransactionButton
    transactionType={TRANSACTION_TYPE_DEPOSIT}
    currencyType={CURRENCY_TYPE_MONEY}
    disabled={REACT_APP_DEPOSIT_BUTTON_DISABLED === "true"}
    title="DEPOSIT (MONEY)"
    description="The money will be deposited in your account in order to pay for paid Challenges."
  >
    Deposit
  </TransactionButton>
);

export default Deposit;
