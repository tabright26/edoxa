import React from "react";
import { Row, Col, Card, CardHeader, CardImg, CardImgOverlay } from "reactstrap";
import { withChallenges } from "store/root/arena/challenges/container";

import ChallengeItem from "./Item/Item";

import ErrorBoundary from "components/Shared/ErrorBoundary";
import Loading from "components/Shared/Loading";

const ArenaChallengeIndex = ({ challenges: { data, error, loading } }) => {
  return (
    <ErrorBoundary>
      {loading ? (
        <Loading />
      ) : (
        <>
          <Row>
            <Col>
              <Card>
                <CardImg src="https://via.placeholder.com/1680x200" height="200" />
                <CardImgOverlay>
                  <h5>League of Legends</h5>
                </CardImgOverlay>
              </Card>
            </Col>
          </Row>
          <Row>
            {data
              .slice()
              .sort((left, right) => void (left.timestamp < right.timestamp ? -1 : 1))
              .map((challenge, index) => (
                <Col key={index} xs="6" sm="4" md="3">
                  <ChallengeItem challenge={challenge} />
                </Col>
              ))}
          </Row>
        </>
      )}
    </ErrorBoundary>
  );
};

export default withChallenges(ArenaChallengeIndex);
