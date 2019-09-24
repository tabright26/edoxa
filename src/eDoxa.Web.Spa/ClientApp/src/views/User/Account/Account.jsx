import React from "react";
import { Container, Row, Col } from "reactstrap";

import StripeBankAccountAlert from "components/Stripe/Bank/Alert/Alert";
import StripeConnectAccountAlert from "components/Stripe/ConnectAccount/Alert/Alert";

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
      {/* <Col xs="6">
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
      </Col> */}
    </Row>
    <Row>
      <Col>
        <StripeBankAccountAlert />
      </Col>
    </Row>
  </Container>
);

export default Account;
