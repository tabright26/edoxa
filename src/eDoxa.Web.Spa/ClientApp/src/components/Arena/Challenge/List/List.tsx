import React from "react";
import { Row, Col, Card, CardHeader } from "reactstrap";
import { withChallenges } from "store/root/arena/challenges/container";

import ChallengeItem from "./Item/Item";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const ArenaChallengeIndex = ({ challenges: { data, error, loading } }) => {
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
        {data
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

export default withChallenges(ArenaChallengeIndex);
