import React from "react";
import Format from "components/Shared/Format";
import connectUserAccountBalance from "containers/connectUserAccountBalance";

const AvailableToken = ({ currency, available }) => <Format.Currency currency={currency} amount={available} justify />;

export default connectUserAccountBalance("token")(AvailableToken);
