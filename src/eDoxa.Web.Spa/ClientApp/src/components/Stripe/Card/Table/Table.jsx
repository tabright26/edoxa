import React from "react";
import { Table } from "reactstrap";

import withStripeCardHoc from "containers/connectStripePaymentMethods";

const StripeCardTable = ({ cards }) => (
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

export default withStripeCardHoc(StripeCardTable);
