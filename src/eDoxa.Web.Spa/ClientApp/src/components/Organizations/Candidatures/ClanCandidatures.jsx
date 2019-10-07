import React, { Fragment, useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectCandidatures } from "store/organizations/candidatures/container";

import ClanCandidatureItem from "./ClanCandidatureItem";

const ClanCandidatures = ({ actions, candidatures, clan, userId, doxaTags }) => {
  useEffect(() => {
    if (clan) {
      actions.loadCandidaturesWithClanId(clan.id);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clan]);

  return (
    <Card>
      <CardHeader>Candidatures</CardHeader>
      <CardBody>
        <Col>
          {candidatures ? (
            candidatures.map((candidature, index) => (
              <Row key={index}>
                <ClanCandidatureItem actions={actions} candidature={candidature} doxaTags={doxaTags} userId={userId} withPermissions={clan ? clan.ownerId === userId : false}></ClanCandidatureItem>
              </Row>
            ))
          ) : (
            <Row></Row>
          )}
        </Col>
      </CardBody>
    </Card>
  );
};

export default connectCandidatures(ClanCandidatures);
