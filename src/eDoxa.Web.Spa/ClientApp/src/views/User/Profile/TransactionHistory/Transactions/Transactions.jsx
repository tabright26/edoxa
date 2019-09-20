import React from "react";
import { Card, CardHeader, Table, Pagination, PaginationItem, PaginationLink } from "reactstrap";
import connectUserAccountTransactions from "containers/connectUserAccountTransactions";
import Moment from "react-moment";
import Format from "components/Shared/Format";

const Transactions = ({ transactions }) => (
  <>
    <Card className="card-accent-primary my-4">
      <CardHeader>
        <strong>TRANSACTIONS</strong>
      </CardHeader>
      <Table className="mb-0" size="sm" responsive striped dark>
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
            ? transactions
                .sort((left, right) => (left.timestamp < right.timestamp ? -1 : 1))
                .map((transaction, index) => (
                  <tr key={index}>
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
