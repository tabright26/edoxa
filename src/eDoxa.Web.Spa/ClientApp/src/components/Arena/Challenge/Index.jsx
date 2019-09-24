import React, { useEffect } from "react";
import { Row, Col, Card, CardHeader } from "reactstrap";
import { connectArenaChallenges } from "store/arena/challenges/container";

import ChallengeItem from "./Item";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ArenaChallengeIndex = ({ actions, challenges }) => {
  useEffect(() => {
    actions.loadChallenges();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <ErrorBoundary>
      <Row>
        <Col>
          <Card className="mt-4">
            <CardHeader tag="h5" className="text-center">
              League of Legends
            </CardHeader>
          </Card>
        </Col>
      </Row>
      <Row>
        {challenges
          .sort((left, right) => (left.timestamp < right.timestamp ? -1 : 1))
          .map((challenge, index) => (
            <Col key={index} xs="6" sm="4" md="3">
              <ChallengeItem challenge={challenge} />
            </Col>
          ))}
      </Row>
    </ErrorBoundary>
  );
};

export default connectArenaChallenges(ArenaChallengeIndex);
