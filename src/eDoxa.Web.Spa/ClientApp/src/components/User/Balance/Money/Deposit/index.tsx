import React, { FunctionComponent } from "react";
import { compose } from "recompose";
import { TRANSACTION_TYPE_DEPOSIT, CURRENCY_TYPE_MONEY } from "types";
import { withStripeCustomerHasDefaultPaymentMethod } from "store/root/payment/stripe/customer/container";
import UserTransactionButton from "components/User/Transaction/Button";

type InnerProps = { hasDefaultPaymentMethod: boolean };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Deposit: FunctionComponent<Props> = ({ hasDefaultPaymentMethod }) => (
  <UserTransactionButton.Create
    transactionType={TRANSACTION_TYPE_DEPOSIT}
    currency={CURRENCY_TYPE_MONEY}
    title="DEPOSIT (MONEY)"
    description="The money will be deposited in your account in order to pay for paid Challenges."
    disabled={!hasDefaultPaymentMethod}
  >
    Deposit
  </UserTransactionButton.Create>
);

const enhance = compose<InnerProps, OutterProps>(
  withStripeCustomerHasDefaultPaymentMethod
);

export default enhance(Deposit);
