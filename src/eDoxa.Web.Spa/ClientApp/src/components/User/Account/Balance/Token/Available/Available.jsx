import React from "react";
import Format from "components/Shared/Format";
import { connectUserAccountBalance } from "store/root/user/account/balance/container";

const AvailableToken = ({ currency, available }) => <Format.Currency currency={currency} amount={available} alignment="justify" />;

export default connectUserAccountBalance("token")(AvailableToken);
