import React from "react";
import Format from "components/Shared/Format";
import connectUserAccountBalanceMoney from "containers/connectUserAccountBalanceMoney";

const AvailableToken = ({ currency, available }) => <Format.Currency currency={currency} amount={available} justify/>;

export default connectUserAccountBalanceMoney(AvailableToken);
