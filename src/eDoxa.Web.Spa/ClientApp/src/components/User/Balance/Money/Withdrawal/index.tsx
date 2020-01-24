import React, { FunctionComponent } from "react";
import { compose } from "recompose";
import { CURRENCY_MONEY, TRANSACTION_TYPE_WITHDRAWAL } from "types";
import { withStripeAccount } from "store/root/payment/stripe/account/container";
import UserTransactionButton from "components/User/Transaction/Button";
import { StripeAccountState } from "store/root/payment/stripe/account/types";

type InnerProps = { account: StripeAccountState };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const WithdrawalButton: FunctionComponent<Props> = ({ account: { data } }) => (
  <UserTransactionButton.Create
    transactionType={TRANSACTION_TYPE_WITHDRAWAL}
    currency={CURRENCY_MONEY}
    disabled={data === null ? true : !data.enabled}
  >
    Withdrawal Money
  </UserTransactionButton.Create>
);

const enhance = compose<InnerProps, OutterProps>(withStripeAccount);

export default enhance(WithdrawalButton);