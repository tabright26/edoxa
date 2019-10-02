import React, { Fragment } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import CandidatureItem from "./CandidatureItem";

const Candidatures = ({ candidatures }) => {
  return (
    <Card className="card-accent-primary">
      <CardHeader>Candidatures</CardHeader>
      <CardBody className="p-1">
        <Col>
          {candidatures.map((candidature, index) => (
            <Fragment>
              <Row className="mt-0 mb-1">
                <CandidatureItem candidature={candidature}></CandidatureItem>
              </Row>
              <hr className="mt-1 mb-0" />
            </Fragment>
          ))}
        </Col>
      </CardBody>
    </Card>
  );
};

export default Candidatures;
