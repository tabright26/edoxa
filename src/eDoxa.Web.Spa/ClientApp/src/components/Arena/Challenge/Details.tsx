import React from "react";
import { Container, Row, Col } from "reactstrap";

import ChallengeLogo from "./Logo";
import ChallengeSetup from "./Specification";
import ChallengeScoreboard from "./Scoreboard";
import ChallengeScoring from "./Scoring";
import ChallengePayout from "./Payout";

import { withChallenge } from "store/root/arena/challenges/container";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const Challenge = ({ challenge }) => {
  return (
    <ErrorBoundary>
      <Container fluid={true}>
        <Row>
          <ChallengeLogo />
          <Col xs={4} className="ml-4">
            <ChallengeSetup challenge={challenge} />
          </Col>
        </Row>
        <Row>
          <Col xs={10}>
            <ChallengeScoreboard challenge={challenge} />
          </Col>
          <Col xs={2}>
            <Row>
              <Col xs={12}>
                <ChallengeScoring challenge={challenge} />
              </Col>
              <Col xs={12}>
                <ChallengePayout challenge={challenge} />
              </Col>
            </Row>
          </Col>
        </Row>
      </Container>
    </ErrorBoundary>
  );
};

export default withChallenge(Challenge);
