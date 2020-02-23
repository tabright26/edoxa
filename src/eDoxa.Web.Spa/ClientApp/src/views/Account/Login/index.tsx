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
import UserAccountForm from "components/Service/Identity/Account/Form";
import { getAccountRegisterPath, getDefaultPath } from "utils/coreui/constants";
import { LinkContainer } from "react-router-bootstrap";
import Layout from "components/App/Layout";
import logo from "assets/img/brand/logo.png";

const Login: FunctionComponent = () => (
  <Layout.Background>
    <Container className="h-100">
      <Row className="justify-content-center">
        <Col md="8 d-flex">
          <LinkContainer to={getDefaultPath()}>
            <CardImg className="mt-5 mb-1 w-75 mx-auto" src={logo} />
          </LinkContainer>
        </Col>
      </Row>
      <Row className="justify-content-center">
        <Col md="8">
          <CardGroup className="my-5">
            <Card className="p-4">
              <CardBody className="d-flex">
                <div className="my-auto w-100">
                  <h1>Login</h1>
                  <p className="text-muted">Sign In to your account</p>
                  <UserAccountForm.Login />
                </div>
              </CardBody>
            </Card>
            <Card
              color="primary"
              className="text-white py-5 d-md-down-none"
              style={{ width: "44%" }}
            >
              <CardBody className="text-center">
                <div className="my-auto w-100">
                  <h2>Sign up</h2>
                  <p className="text-justify">
                    eDoxa wants to help unlock your potential so that you can be
                    proud to live your eSport dream.
                  </p>
                  <p className="text-justify">
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
