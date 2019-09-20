import React from "react";
import Format from "components/Shared/Format";
import connectUserAccountBalanceToken from "containers/connectUserAccountBalanceToken";

const AvailableToken = ({ currency, available }) => <Format.Currency currency={currency} amount={available} justify />;

export default connectUserAccountBalanceToken(AvailableToken);
