import React from "react";
import { Container, CardHeader, CardBody, Row, Col, Card } from "reactstrap";

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
        <Card color="dark" className="text-light mt-3">
          <CardHeader tag="h3">Money</CardHeader>
          <CardBody>
            <UserAccountBalanceMoney />
          </CardBody>
        </Card>
      </Col>
      <Col xs="6">
        <Card color="dark" className="text-light mt-3">
          <CardHeader tag="h3">Tokens</CardHeader>
          <CardBody>
            <UserAccountBalanceToken />
          </CardBody>
        </Card>
      </Col>
    </Row>
    <Row>
      <Col xs="12">
        <h3 className="text-light mt-4">Transactions</h3>
        <hr className="border" />
      </Col>
      <Col>
        <Card color="dark">
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
        <Card color="dark">
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
