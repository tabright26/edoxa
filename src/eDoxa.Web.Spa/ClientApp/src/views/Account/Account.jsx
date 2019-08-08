import React from "react";
import { Container, Row, Col, Card } from "react-bootstrap";

import UserAccountBalanceMoney from "../../components/User/Account/Balance/Money/Index";
import UserAccountBalanceToken from "../../components/User/Account/Balance/Token/Index";
import UserAccountTransactionTable from "../../components/User/Account/Transaction/Table";
import UserAccountStripeCardTable from "../../components/User/Account/Stripe/Card/Table";
import UserAccountStripeBankAccountAlert from "../../components/User/Account/Stripe/BankAccount/Alert";
import UserAccountStripeConnectAccountAlert from "../../components/User/Account/Stripe/ConnectAccount/Alert";

const Account = () => (
  <Container>
    <Row>
      <Col xs="12">
        <h1 className="text-light mt-5">Account Overview</h1>
        <hr className="border" />
      </Col>
      <Col>
        <UserAccountStripeConnectAccountAlert />
      </Col>
    </Row>
    <Row>
      <Col xs="6">
        <Card bg="dark" className="text-light mt-3">
          <Card.Header as="h3">Money</Card.Header>
          <Card.Body>
            <UserAccountBalanceMoney />
          </Card.Body>
        </Card>
      </Col>
      <Col xs="6">
        <Card bg="dark" className="text-light mt-3">
          <Card.Header as="h3">Tokens</Card.Header>
          <Card.Body>
            <UserAccountBalanceToken />
          </Card.Body>
        </Card>
      </Col>
    </Row>
    <Row>
      <Col xs="12">
        <h3 className="text-light mt-4">Transactions</h3>
        <hr className="border" />
      </Col>
      <Col>
        <Card bg="dark">
          <UserAccountTransactionTable variant="dark" className="mb-0" borderless hover responsive striped />
        </Card>
      </Col>
    </Row>
    <Row>
      <Col xs="12">
        <h3 className="text-light mt-4">Payment Methods</h3>
        <hr className="border" />
      </Col>
      <Col>
        <Card bg="dark">
          <UserAccountStripeCardTable />
        </Card>
      </Col>
    </Row>
    <Row>
      <Col>
        <UserAccountStripeBankAccountAlert />
      </Col>
    </Row>
  </Container>
);

export default Account;
