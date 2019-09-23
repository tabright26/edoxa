import React from "react";
import Format from "components/Shared/Format";
import connectUserAccountBalance from "store/user/account/balance/container";

const PendingToken = ({ currency, pending }) => <Format.Currency currency={currency} amount={pending} justify />;

export default connectUserAccountBalance("money")(PendingToken);
