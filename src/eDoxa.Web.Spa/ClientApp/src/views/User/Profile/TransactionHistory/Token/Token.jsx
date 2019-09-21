import React from "react";
import { Card, CardHeader, Table, Pagination, PaginationItem, PaginationLink, Badge } from "reactstrap";
import connectUserAccountTransactions from "containers/connectUserAccountTransactions";
import Moment from "react-moment";
import Format from "components/Shared/Format";

const Transactions = ({ transactions }) => (
  <>
    <Card className="card-accent-primary my-4">
      <CardHeader>
        <strong>TOKEN TRANSACTIONS</strong>
      </CardHeader>
      <Table className="mb-0" responsive striped dark>
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
            ? transactions
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
                      <Format.Currency currency={transaction.currency} amount={transaction.amount} />
                    </td>
                    <td className="my-auto">{transaction.description}</td>
                  </tr>
                ))
            : []}
        </tbody>
      </Table>
    </Card>
    <Pagination>
      <PaginationItem disabled>
        <PaginationLink previous tag="button">
          Prev
        </PaginationLink>
      </PaginationItem>
      <PaginationItem active>
        <PaginationLink tag="button">1</PaginationLink>
      </PaginationItem>
      <PaginationItem>
        <PaginationLink tag="button">2</PaginationLink>
      </PaginationItem>
      <PaginationItem>
        <PaginationLink tag="button">3</PaginationLink>
      </PaginationItem>
      <PaginationItem>
        <PaginationLink tag="button">4</PaginationLink>
      </PaginationItem>
      <PaginationItem>
        <PaginationLink next tag="button">
          Next
        </PaginationLink>
      </PaginationItem>
    </Pagination>
  </>
);

export default connectUserAccountTransactions(Transactions);
