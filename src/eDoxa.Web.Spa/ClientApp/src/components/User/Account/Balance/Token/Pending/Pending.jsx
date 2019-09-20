import React from "react";
import Format from "components/Shared/Format";
import connectUserAccountBalanceToken from "containers/connectUserAccountBalanceToken";

const PendingToken = ({ currency, pending }) => <Format.Currency currency={currency} amount={pending} justify />;

export default connectUserAccountBalanceToken(PendingToken);
