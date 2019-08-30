import React from "react";
import { Card, CardBody, Col, Container, Row } from "reactstrap";
import { Redirect } from "react-router-dom";
import SecurityPassword from "../../../../../forms/Security/Password";
import withUserContainer from "../../../../../containers/App/User/withUserContainer";
import queryString from "query-string";

const ResetPassword = ({ location, actions }) => {
  const code = queryString.parse(location.search).code;
  if (!code) {
    return <Redirect to="/errors/404" />;
  }
  return (
    <div className="app flex-row align-items-center">
      <Container>
        <Row className="justify-content-center">
          <Col md="9" lg="7" xl="6">
            <Card className="mx-4">
              <CardBody className="p-4">
                <h1>Reset Password</h1>
                <p className="text-muted">Reset your account password</p>
                <SecurityPassword.Reset onSubmit={fields => actions.resetPassword(fields, code)} />
              </CardBody>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default withUserContainer(ResetPassword);
