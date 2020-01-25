import React, { FunctionComponent } from "react";
import { compose } from "recompose";
import { CURRENCY_TOKEN, TRANSACTION_TYPE_DEPOSIT } from "types";
import { withStripeCustomerHasDefaultPaymentMethod } from "store/root/payment/stripe/customer/container";
import UserTransactionButton from "components/User/Transaction/Button";

type InnerProps = { hasDefaultPaymentMethod: boolean };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Buy: FunctionComponent<Props> = ({ hasDefaultPaymentMethod }) => (
  <UserTransactionButton.Create
    transactionType={TRANSACTION_TYPE_DEPOSIT}
    currency={CURRENCY_TOKEN}
    title="BUY (TOKEN)"
    disabled={!hasDefaultPaymentMethod}
  >
    Buy
  </UserTransactionButton.Create>
);

const enhance = compose<InnerProps, OutterProps>(
  withStripeCustomerHasDefaultPaymentMethod
);

export default enhance(Buy);
