import React, { FunctionComponent } from "react";
import {
  Button,
  Card,
  CardBody,
  CardGroup,
  Col,
  Container,
  Row,
  CardImg
} from "reactstrap";
import UserAccountForm from "components/Account/Form";
import { getAccountRegisterPath, getHomePath } from "utils/coreui/constants";
import { LinkContainer } from "react-router-bootstrap";
import Layout from "components/Shared/Layout";
import logo from "assets/img/brand/logo.png";

const Login: FunctionComponent = () => (
  <Layout.Background>
    <Container className="h-100">
      <Row className="justify-content-center">
        <Col md="8">
          <LinkContainer to={getHomePath()}>
            <CardImg className="my-5" src={logo} />
          </LinkContainer>
        </Col>
      </Row>
      <Row className="justify-content-center">
        <Col md="8">
          <CardGroup className="my-5">
            <Card className="p-4">
              <CardBody>
                <h1>Login</h1>
                <p className="text-muted">Sign In to your account</p>
                <UserAccountForm.Login />
              </CardBody>
            </Card>
            <Card
              className="text-white bg-primary py-5 d-md-down-none"
              style={{ width: "44%" }}
            >
              <CardBody className="text-center">
                <div>
                  <h2>Sign up</h2>
                  <p>
                    eDoxa wants to help unlock your potential so that you can be
                    proud to live your eSport dream.
                  </p>
                  <p>
                    Participate to Challenges for the opportunity of making
                    money and change the perception of eSport a dollar at a
                    time!
                  </p>
                  <LinkContainer to={getAccountRegisterPath()}>
                    <Button
                      color="primary"
                      className="mt-3"
                      active
                      tabIndex={-1}
                    >
                      Register Now!
                    </Button>
                  </LinkContainer>
                </div>
              </CardBody>
            </Card>
          </CardGroup>
        </Col>
      </Row>
    </Container>
  </Layout.Background>
);

export default Login;
