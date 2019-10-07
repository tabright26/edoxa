import React, { useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectCandidatures } from "store/organizations/candidatures/container";

import ClanCandidatureItem from "./ClanCandidatureItem";

const ClanCandidatures = ({ actions, candidatures, clanId, doxaTags, isOwner }) => {
  useEffect(() => {
    if (clanId) {
      actions.loadCandidaturesWithClanId(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  return (
    <Card>
      <CardHeader>Candidatures</CardHeader>
      <CardBody>
        <Col>
          {candidatures
            ? candidatures.map((candidature, index) => (
                <Row key={index}>
                  <ClanCandidatureItem actions={actions} candidature={candidature} doxaTags={doxaTags} isOwner={isOwner}></ClanCandidatureItem>
                </Row>
              ))
            : ""}
        </Col>
      </CardBody>
    </Card>
  );
};

export default connectCandidatures(ClanCandidatures);
