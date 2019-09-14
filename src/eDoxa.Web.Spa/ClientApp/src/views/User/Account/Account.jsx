import React from "react";
import { Container, Row, Col, Card } from "react-bootstrap";

import UserAccountBalanceMoney from "../../../components/User/Account/Balance/Money/Index";
import UserAccountBalanceToken from "../../../components/User/Account/Balance/Token/Index";
import UserAccountTransactionTable from "../../../components/User/Account/Transaction/Table/Table";
import StripeCardTable from "../../../components/Stripe/Card/Table/Table";
import StripeBankAccountAlert from "../../../components/Stripe/Bank/Alert/Alert";
import StripeConnectAccountAlert from "../../../components/Stripe/ConnectAccount/Alert/Alert";

const Account = () => (
  <Container>
    <Row>
      <Col xs="12">
        <h1 className="text-light mt-5">Account Overview</h1>
        <hr className="border" />
      </Col>
      <Col>
        <StripeConnectAccountAlert />
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
          <UserAccountTransactionTable />
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
          <StripeCardTable />
        </Card>
      </Col>
    </Row>
    <Row>
      <Col>
        <StripeBankAccountAlert />
      </Col>
    </Row>
  </Container>
);

export default Account;
