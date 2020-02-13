import React, { FunctionComponent } from "react";
import { Card, CardBody, Col, Container, Row, CardImg } from "reactstrap";
import UserAccountForm from "components/Account/Form";
import logo from "assets/img/brand/logo.png";
import Layout from "components/Shared/Layout";
import { LinkContainer } from "react-router-bootstrap";
import { getDefaultPath } from "utils/coreui/constants";

const Register: FunctionComponent = () => (
  <Layout.Background>
    <Container>
      <Row className="justify-content-center">
        <Col md="9" lg="7" xl="6">
          <LinkContainer to={getDefaultPath()}>
            <CardImg className="my-5" src={logo} />
          </LinkContainer>
        </Col>
      </Row>
      <Row className="justify-content-center">
        <Col md="9" lg="7" xl="6">
          <Card className="mx-4 my-5">
            <CardBody className="p-4">
              <h1>Register</h1>
              <p className="text-muted">Create your account</p>
              <UserAccountForm.Register />
            </CardBody>
            {/* <CardFooter className="p-4">
                <Row>
                  <Col xs="12" sm="6">
                    <Button className="btn-facebook mb-1" block>
                      <span>facebook</span>
                    </Button>
                  </Col>
                  <Col xs="12" sm="6">
                    <Button className="btn-twitter mb-1" block>
                      <span>twitter</span>
                    </Button>
                  </Col>
                </Row>
              </CardFooter> */}
          </Card>
        </Col>
      </Row>
    </Container>
  </Layout.Background>
);

export default Register;
