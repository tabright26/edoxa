import React, { FunctionComponent } from "react";
import { Table, Badge } from "reactstrap";
import Moment from "react-moment";
import Format from "components/Shared/Format";
import { UserTransaction } from "types";

interface Props {
  transactions: UserTransaction[];
}

const TransactionList: FunctionComponent<Props> = ({ transactions }) => (
  <Table className="mb-0" responsive striped hover dark>
    <thead>
      <tr>
        <th>Date</th>
        <th>Status</th>
        <th>Type</th>
        <th>Amount</th>
      </tr>
    </thead>
    <tbody>
      {transactions.map((transaction, index) => (
        <tr key={index}>
          <td className="my-auto">
            <Moment unix format="lll">
              {transaction.timestamp}
            </Moment>
          </td>
          <td className="my-auto">
            <Badge color="primary">{transaction.status}</Badge>
          </td>
          <td className="my-auto">{transaction.type}</td>
          <td className="my-auto">
            <Badge className="bg-gray-900 w-100">
              <Format.Currency
                currency={transaction.currency}
                amount={transaction.amount}
                alignment="justify"
              />
            </Badge>
          </td>
        </tr>
      ))}
    </tbody>
  </Table>
);

export default TransactionList;
