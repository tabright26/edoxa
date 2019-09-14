import React from "react";
import { Table } from "reactstrap";

import Format from "../../../Shared/Format";

const Transaction = ({ transaction }) => {
  return (
    <Table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Date</th>
          <th>Amount</th>
          <th>Description</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>{transaction.id}</td>
          <td>{transaction.timestamp}</td>
          <td>
            <Format.Currency currency={transaction.currency} amount={transaction.amount} />
          </td>
          <td>{transaction.description}</td>
          <td>{transaction.status}</td>
        </tr>
      </tbody>
    </Table>
  );
};

export default Transaction;
