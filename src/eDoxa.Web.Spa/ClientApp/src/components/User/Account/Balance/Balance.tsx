import React, { FunctionComponent } from "react";
import Format from "components/Shared/Format";
import { withUserAccountBalance } from "store/root/user/account/balance/container";
import { compose } from "recompose";
import { UserAccountBalanceState } from "store/root/user/account/balance/types";
import { Currency } from "types";

interface InnerProps {
  balance: UserAccountBalanceState;
}

interface OutterProps {
  currency: Currency;
  attribute: "available" | "pending";
}

type Props = InnerProps & OutterProps;

const UserAccountBalance: FunctionComponent<Props> = ({ currency, balance: { data, error, loading }, attribute }) => (
  <Format.Currency currency={currency} amount={data[attribute]} alignment="justify" />
);

const enhance = compose<InnerProps, OutterProps>(withUserAccountBalance);

export default enhance(UserAccountBalance);
