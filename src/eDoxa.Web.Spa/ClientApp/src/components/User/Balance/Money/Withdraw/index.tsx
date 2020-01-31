import React, { FunctionComponent } from "react";
import { compose } from "recompose";
import { CURRENCY_TYPE_MONEY, TRANSACTION_TYPE_WITHDRAWAL } from "types";
import { withStripeAccount } from "store/root/payment/stripe/account/container";
import UserTransactionButton from "components/User/Transaction/Button";
import { StripeAccountState } from "store/root/payment/stripe/account/types";

type InnerProps = { account: StripeAccountState };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Withdraw: FunctionComponent<Props> = ({ account: { data } }) => (
  <UserTransactionButton.Create
    transactionType={TRANSACTION_TYPE_WITHDRAWAL}
    currency={CURRENCY_TYPE_MONEY}
    disabled={data === null ? true : !data.enabled}
    title="WITHDRAL (MONEY)"
    description="We withdraw the money from your cashier and deposit it in your bank account for your personal usage."
  >
    Withdraw
  </UserTransactionButton.Create>
);

const enhance = compose<InnerProps, OutterProps>(withStripeAccount);

export default enhance(Withdraw);
