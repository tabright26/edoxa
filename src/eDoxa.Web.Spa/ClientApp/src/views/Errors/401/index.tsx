import React, { FunctionComponent } from "react";
import { Col, Container, Row } from "reactstrap";

const Error401: FunctionComponent = () => (
  <div className="app flex-row align-items-center">
    <Container>
      <Row className="justify-content-center">
        <Col md="6">
          <div className="clearfix">
            <h1 className="float-left display-3 mr-4">401</h1>
            <h4 className="pt-3">Oops! You are unauthorized.</h4>
            <p className="text-muted float-left">
              The page you need requires authorization.
            </p>
          </div>
        </Col>
      </Row>
    </Container>
  </div>
);

export default Error401;
