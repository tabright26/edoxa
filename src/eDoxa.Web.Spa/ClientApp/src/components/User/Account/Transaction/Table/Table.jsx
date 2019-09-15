import React from "react";
import { Table } from "reactstrap";

import UserAccountTransactionTableRow from "./Row/Row";

import withUserAccountTransactionHoc from "containers/withUserAccountTransactionHoc";

const UserAccountTransactionTable = ({ transactions }) => {
  return (
    <Table className="mb-0" responsive striped dark>
      <thead>
        <tr>
          <th>Id</th>
          <th>Date</th>
          <th>Amount</th>
          <th>Description</th>
          <th>Type</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        {transactions
          .sort((left, right) => (left.timestamp < right.timestamp ? -1 : 1))
          .map((transaction, index) => (
            <UserAccountTransactionTableRow key={index} transaction={transaction} />
          ))}
      </tbody>
    </Table>
  );
};

export default withUserAccountTransactionHoc(UserAccountTransactionTable);
