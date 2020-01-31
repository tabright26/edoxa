import React, { FunctionComponent } from "react";
import Format from "components/Shared/Format";
import { withUserAccountBalance } from "store/root/user/balance/container";
import { compose } from "recompose";
import { UserBalanceState } from "store/root/user/balance/types";
import { CurrencyType } from "types";

interface InnerProps {
  balance: UserBalanceState;
}

interface OutterProps {
  type: CurrencyType;
  attribute: "available" | "pending";
  alignment?: "right" | "left" | "center" | "justify";
}

type Props = InnerProps & OutterProps;

const Balance: FunctionComponent<Props> = ({
  type,
  balance: { data },
  attribute,
  alignment = "justify"
}) => {
  console.log(data);
  return (
    <Format.Currency
      currency={{
        type,
        amount: data[attribute]
      }}
      alignment={alignment}
    />
  );
};

const enhance = compose<InnerProps, OutterProps>(withUserAccountBalance);

export default enhance(Balance);
