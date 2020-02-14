import React, { FunctionComponent } from "react";
import { TRANSACTION_TYPE_DEPOSIT, CURRENCY_TYPE_MONEY } from "types";
import TransactionButton from "components/Transaction/Button";

const Deposit: FunctionComponent = () => (
  <TransactionButton
    transactionType={TRANSACTION_TYPE_DEPOSIT}
    currencyType={CURRENCY_TYPE_MONEY}
    title="DEPOSIT (MONEY)"
    description="The money will be deposited in your account in order to pay for paid Challenges."
  >
    Deposit
  </TransactionButton>
);

export default Deposit;
