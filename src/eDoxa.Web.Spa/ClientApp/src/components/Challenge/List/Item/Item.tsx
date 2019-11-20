import React from "react";
import { Card, CardBody, Row, Col } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import ChallengeLogo from "components/Challenge/Logo/Logo";
import ChallengeSummary from "components/Challenge/Summary";

const ChallengeParticipantItem = ({ challenge }) => {
  return (
    <Card>
      <LinkContainer
        to={"/arena/challenges/" + challenge.id}
        style={{
          cursor: "pointer"
        }}
      >
        <CardBody>
          <Row>
            <Col md="3">
              <ChallengeLogo
                className="h-100 bg-gray-900"
                height={150}
                width={150}
              />
            </Col>
            <Col md="9">
              <ChallengeSummary challengeId={challenge.id} />
            </Col>
          </Row>
        </CardBody>
      </LinkContainer>
    </Card>
  );
};

export default ChallengeParticipantItem;