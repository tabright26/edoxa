import React, { FunctionComponent } from "react";
import { CURRENCY_TYPE_MONEY, TRANSACTION_TYPE_WITHDRAWAL } from "types";
import UserTransactionButton from "components/Transaction/Button";

const Withdraw: FunctionComponent = () => (
  <UserTransactionButton.Create
    transactionType={TRANSACTION_TYPE_WITHDRAWAL}
    currency={CURRENCY_TYPE_MONEY}
    title="WITHDRAW (MONEY)"
    description="We withdraw the money from your cashier and deposit it in your bank account for your personal usage."
  >
    Withdraw
  </UserTransactionButton.Create>
);

export default Withdraw;
