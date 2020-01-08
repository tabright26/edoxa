import React, { FunctionComponent } from "react";
import { compose } from "recompose";
import { CURRENCY_MONEY, TRANSACTION_TYPE_WITHDRAWAL } from "types";
import { withStripeHasAccountEnabled } from "store/root/payment/stripe/account/container";
import UserTransactionButton from "components/User/Transaction/Button";

type InnerProps = { hasAccountVerified: boolean };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const WithdrawalButton: FunctionComponent<Props> = ({ hasAccountVerified }) => (
  <UserTransactionButton.Create
    transactionType={TRANSACTION_TYPE_WITHDRAWAL}
    currency={CURRENCY_MONEY}
    disabled={!hasAccountVerified}
  >
    Withdrawal Money
  </UserTransactionButton.Create>
);

const enhance = compose<InnerProps, OutterProps>(withStripeHasAccountEnabled);

export default enhance(WithdrawalButton);
