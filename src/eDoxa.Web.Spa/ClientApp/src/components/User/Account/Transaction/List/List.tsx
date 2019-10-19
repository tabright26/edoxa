import React, { FunctionComponent } from "react";
import { Table, Badge } from "reactstrap";
import Moment from "react-moment";
import Format from "components/Shared/Format";
import { Transaction } from "types";

interface Props {
  transactions: Transaction[];
}

const TransactionList: FunctionComponent<Props> = ({ transactions }) => (
  <Table className="mb-0" responsive striped hover dark>
    <thead>
      <tr>
        <th>Date</th>
        <th>Status</th>
        <th>Type</th>
        <th>Amount</th>
        <th>Description</th>
      </tr>
    </thead>
    <tbody>
      {transactions
        .sort((left, right) => (left.timestamp < right.timestamp ? -1 : 1))
        .map((transaction, index) => (
          <tr key={index}>
            <td className="my-auto">
              <Moment unix format="ll">
                {transaction.timestamp}
              </Moment>
            </td>
            <td className="my-auto">
              <Badge color="primary">{transaction.status}</Badge>
            </td>
            <td className="my-auto">{transaction.type}</td>
            <td className="my-auto">
              <Format.Currency currency={transaction.currency} amount={transaction.amount} alignment="left" />
            </td>
            <td className="my-auto">{transaction.description}</td>
          </tr>
        ))}
    </tbody>
  </Table>
);

export default TransactionList;
