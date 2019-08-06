import React from 'react';
import { Table } from 'react-bootstrap';
import { Moment } from 'react-moment';

import CurrencyFormat from '../../../Shared/Formaters/CurrencyFormat';

import withUserAccountTransactionContainer from './Container';

const UserAccountTransactionTable = ({ transactions, ...rest }) => {
  return (
    <Table {...rest}>
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
            <tr key={index}>
              <td>{transaction.id}</td>
              <td>
                <Moment unix format="ll">
                  {transaction.timestamp}
                </Moment>
              </td>
              <td>
                <CurrencyFormat
                  currency={transaction.currency}
                  amount={transaction.amount}
                />
              </td>
              <td>{transaction.description}</td>
              <td>{transaction.type}</td>
              <td>{transaction.status}</td>
            </tr>
          ))}
      </tbody>
    </Table>
  );
};

export default withUserAccountTransactionContainer(UserAccountTransactionTable);
