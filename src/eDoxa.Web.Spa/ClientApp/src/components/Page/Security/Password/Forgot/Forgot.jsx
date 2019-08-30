import React from "react";
import { Card, CardBody, Col, Container, Row } from "reactstrap";
import SecurityPassword from "../../../../../forms/Security/Password";
import withForgotPassword from "../../../../../containers/App/Security/withForgotPassword";

const ForgotPassword = ({ actions }) => (
  <div className="app flex-row align-items-center">
    <Container>
      <Row className="justify-content-center">
        <Col md="9" lg="7" xl="6">
          <Card className="mx-4">
            <CardBody className="p-4">
              <h1>Forgot Password</h1>
              <p className="text-muted">Forgot your account password</p>
              <SecurityPassword.Forgot onSubmit={fields => actions.forgotPassword(fields)} />
            </CardBody>
          </Card>
        </Col>
      </Row>
    </Container>
  </div>
);

export default withForgotPassword(ForgotPassword);
