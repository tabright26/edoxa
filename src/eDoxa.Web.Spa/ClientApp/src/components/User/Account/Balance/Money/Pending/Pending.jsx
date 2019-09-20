import React from "react";
import Format from "components/Shared/Format";
import connectUserAccountBalanceMoney from "containers/connectUserAccountBalanceMoney";

const PendingToken = ({ currency, pending }) => <Format.Currency currency={currency} amount={pending} justify />;

export default connectUserAccountBalanceMoney(PendingToken);
