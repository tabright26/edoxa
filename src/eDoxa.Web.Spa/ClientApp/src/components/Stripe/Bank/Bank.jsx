import React from "react";
import { Col, Row, Input } from "reactstrap";

const Bank = ({ bank }) => {
  const ibanNumber = "XXXX XXXX XXXX XXXX XXXX XXX" + bank.last4.charAt(1) + " " + bank.last4.substring(1, 4);
  return (
    <Row>
      <Col xs="12">
        <label>IBAN Number</label>
        <Input type="text" value={ibanNumber} bsSize="sm" disabled />
      </Col>
      <Col xs="12">
        <label>Account Holder Name</label>
        <Input type="text" value={bank.account_holder_name ? bank.account_holder_name : ""} bsSize="sm" disabled />
      </Col>
      <Col xs="12">
        <label>Country</label>
        <Input type="text" value={bank.country} bsSize="sm" disabled />
      </Col>
      <Col xs="12">
        <label>Bank Name</label>
        <Input type="text" value={bank.bank_name} bsSize="sm" disabled />
      </Col>
      <Col xs="12">
        <label>Currency</label>
        <Input type="text" value={bank.currency} bsSize="sm" disabled />
      </Col>
    </Row>
  );
};

export default Bank;
