import React from "react";

import CurrencyFormat from "../../../../../containers/Shared/Formats/Currency";

const UserAccountBalanceCurrency = ({ currency }) => (
  <dl className="row mb-0">
    <dt className="col-sm-9">Available</dt>
    <dd className="col-sm-3">
      <CurrencyFormat currency={currency.type} amount={currency.available} />
    </dd>
    <dt className="col-sm-9">Pending</dt>
    <dd className="col-sm-3 mb-0">
      <CurrencyFormat currency={currency.type} amount={currency.pending} />
    </dd>
  </dl>
);

export default UserAccountBalanceCurrency;
