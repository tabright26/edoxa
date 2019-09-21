import React from "react";
import Format from "components/Shared/Format";
import connectUserAccountBalance from "containers/connectUserAccountBalance";

const PendingToken = ({ currency, pending }) => <Format.Currency currency={currency} amount={pending} justify />;

export default connectUserAccountBalance("money")(PendingToken);
