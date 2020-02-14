import React, { FunctionComponent } from "react";
import { CURRENCY_TYPE_MONEY, TRANSACTION_TYPE_WITHDRAWAL } from "types";
import TransactionButton from "components/Transaction/Button";

const Withdraw: FunctionComponent = () => (
  <TransactionButton
    transactionType={TRANSACTION_TYPE_WITHDRAWAL}
    currencyType={CURRENCY_TYPE_MONEY}
    title="WITHDRAW (MONEY)"
    description="We withdraw the money from your cashier and deposit it in your bank account for your personal usage."
  >
    Withdraw
  </TransactionButton>
);

export default Withdraw;
