import React, { FunctionComponent } from "react";
import TransactionButton from "components/Transaction/Button";
import { REACT_APP_WITHDRAW_BUTTON_DISABLED } from "keys";
import {
  TRANSACTION_TYPE_WITHDRAWAL,
  CURRENCY_TYPE_MONEY
} from "types/cashier";

const Withdraw: FunctionComponent = () => (
  <TransactionButton
    transactionType={TRANSACTION_TYPE_WITHDRAWAL}
    currencyType={CURRENCY_TYPE_MONEY}
    disabled={REACT_APP_WITHDRAW_BUTTON_DISABLED === "true"}
    title="WITHDRAW (MONEY)"
    description="We withdraw the money from your cashier and deposit it in your bank account for your personal usage."
  >
    Withdraw
  </TransactionButton>
);

export default Withdraw;
