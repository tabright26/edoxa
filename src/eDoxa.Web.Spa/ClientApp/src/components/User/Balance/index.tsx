import React, { FunctionComponent } from "react";
import Format from "components/Shared/Format";
import { withUserAccountBalance } from "store/root/user/balance/container";
import { compose } from "recompose";
import { UserBalanceState } from "store/root/user/balance/types";
import { Currency } from "types";

interface InnerProps {
  balance: UserBalanceState;
}

interface OutterProps {
  currency: Currency;
  attribute: "available" | "pending";
  alignment?: "right" | "left" | "center" | "justify";
}

type Props = InnerProps & OutterProps;

const Balance: FunctionComponent<Props> = ({ currency, balance: { data }, attribute, alignment = "justify" }) => (
  <Format.Currency currency={currency} amount={data[attribute]} alignment={alignment} />
);

const enhance = compose<InnerProps, OutterProps>(withUserAccountBalance);

export default enhance(Balance);
