import React, { FunctionComponent } from "react";
import Format from "components/Shared/Format";
import { withUserAccountBalance } from "store/root/user/account/balance/container";
import { compose } from "recompose";
import { UserAccountBalanceState } from "store/root/user/account/balance/types";
import { Currency } from "types";

interface UserAccountBalanceProps {
  currency: Currency;
  balance: UserAccountBalanceState;
  attribute: "available" | "pending";
}

const UserAccountBalance: FunctionComponent<UserAccountBalanceProps> = ({ currency, balance: { data, error, loading }, attribute }) => (
  <Format.Currency currency={currency} amount={data[attribute]} alignment="justify" />
);

const enhance = compose<any, any>(withUserAccountBalance);

export default enhance(UserAccountBalance);
