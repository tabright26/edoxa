import React from "react";
import { Col, Row, Input } from "reactstrap";

const Card = ({ card }) => {
  const cardNumber = "XXXX XXXX XXXX ".concat(card.last4);
  return (
    <Row>
      <Col xs="12">
        <label>Card Number</label>
        <Input type="text" value={cardNumber} bsSize="sm" disabled />
      </Col>
      <Col xs="12">
        <label>Cardholder Name</label>
        <Input type="text" value={card.name ? card.name : ""} bsSize="sm" disabled />
      </Col>
      <Col xs="12">
        <label>Exp.Month</label>
        <Input type="text" value={card.exp_month} bsSize="sm" disabled />
      </Col>
      <Col xs="12">
        <label>Exp.Year</label>
        <Input type="text" value={card.exp_year} bsSize="sm" disabled />
      </Col>
      <Col xs="12">
        <label>Brand</label>
        <Input type="text" value={card.brand} bsSize="sm" disabled />
      </Col>
    </Row>
  );
};

export default Card;
