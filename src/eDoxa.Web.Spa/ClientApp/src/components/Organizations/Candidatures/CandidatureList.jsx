import React from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectCandidatures } from "store/root/organizations/candidatures/container";

import CandidatureItem from "./CandidatureItem";

const CandidatureList = ({ actions, candidatures, type, isOwner = false }) => (
  <Card>
    <CardHeader>Candidatures</CardHeader>
    <CardBody>
      <Col>
        {candidatures
          ? candidatures.map((candidature, index) => (
              <Row key={index}>
                <CandidatureItem actions={actions} candidature={candidature} type={type} isOwner={isOwner}></CandidatureItem>
              </Row>
            ))
          : null}
      </Col>
    </CardBody>
  </Card>
);
export default connectCandidatures(CandidatureList);
