import React from "react";
import Format from "components/Shared/Format";
import { connectUserAccountBalance } from "store/root/user/account/balance/container";

const PendingToken = ({ currency, pending }) => <Format.Currency currency={currency} amount={pending} alignment="justify" />;

export default connectUserAccountBalance("token")(PendingToken);
