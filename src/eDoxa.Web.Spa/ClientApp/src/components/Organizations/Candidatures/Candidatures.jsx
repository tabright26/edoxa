import React, { Fragment, useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectCandidatures } from "store/organizations/candidatures/container";

import CandidatureItem from "./CandidatureItem";

const Candidatures = ({ actions, candidatures, clanId }) => {
  useEffect(() => {
    actions.loadCandidaturesWithClanId(clanId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

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

export default connectCandidatures(Candidatures);
