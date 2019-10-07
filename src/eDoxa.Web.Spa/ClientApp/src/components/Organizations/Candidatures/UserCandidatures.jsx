import React, { useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectCandidatures } from "store/organizations/candidatures/container";

import UserCandidatureItem from "./UserCandidatureItem";

const UserCandidatures = ({ actions, candidatures, userId }) => {
  useEffect(() => {
    actions.loadCandidaturesWithUserId(userId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [userId]);

  return (
    <Card>
      <CardHeader>Candidatures</CardHeader>
      <CardBody>
        <Col>
          {candidatures
            ? candidatures.map((candidature, index) => (
                <Row key={index}>
                  <UserCandidatureItem candidature={candidature}></UserCandidatureItem>
                </Row>
              ))
            : ""}
        </Col>
      </CardBody>
    </Card>
  );
};

export default connectCandidatures(UserCandidatures);
