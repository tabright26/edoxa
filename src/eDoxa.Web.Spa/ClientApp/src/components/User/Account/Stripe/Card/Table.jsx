import React from 'react';
import { Table } from 'react-bootstrap';

import withUserAccountStripeCardContainer from './Container';

const UserAccountStripeCardTable = ({ cards }) => (
  <Table variant="dark" responsive borderless striped hover className="mb-0">
    <thead>
      <tr>
        <th>Brand</th>
        <th>Last4</th>
        <th>Expiration</th>
      </tr>
    </thead>
    <tbody>
      {cards.map((card, index) => (
        <tr key={index}>
          <td>{card.brand}</td>
          <td>**** {card.last4}</td>
          <td>
            {card.exp_month}/{card.exp_year}
          </td>
        </tr>
      ))}
    </tbody>
  </Table>
);

export default withUserAccountStripeCardContainer(UserAccountStripeCardTable);
