import React from "react";
import { Moment } from "react-moment";

import Format from "../../../../../../../components/Format";

const UserAccountTransactionTableRow = ({ transaction }) => {
  return (
    <tr>
      <td>{transaction.id}</td>
      <td>
        <Moment unix format="ll">
          {transaction.timestamp}
        </Moment>
      </td>
      <td>
        <Format.Currency currency={transaction.currency} amount={transaction.amount} />
      </td>
      <td>{transaction.description}</td>
      <td>{transaction.type}</td>
      <td>{transaction.status}</td>
    </tr>
  );
};

export default UserAccountTransactionTableRow;
