import React, { FunctionComponent } from "react";
import Format from "components/Shared/Format";
import { connectUserAccountBalance } from "store/root/user/account/balance/container";
import { compose } from "recompose";

const UserAccountBalance: FunctionComponent<any> = ({ currency, available, pending, selector }) => {
  switch (selector) {
    case "available":
      return <Format.Currency currency={currency} amount={available} alignment="justify" />;
    case "pending":
      return <Format.Currency currency={currency} amount={pending} alignment="justify" />;
    default:
      throw new Error("Invalid balance.");
  }
};

const enhance = compose<any, any>(connectUserAccountBalance);

export default enhance(UserAccountBalance);
