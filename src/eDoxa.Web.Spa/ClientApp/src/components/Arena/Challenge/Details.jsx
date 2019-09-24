import React, { useState, useEffect } from "react";
import { Container, Row, Col } from "reactstrap";

import ChallengeLogo from "./Logo";
import ChallengeSetup from "./Specification";
import ChallengeScoreboard from "./Scoreboard";
import ChallengeScoring from "./Scoring";
import ChallengePayout from "./Payout";

import { connectArenaChallenges } from "store/arena/challenges/container";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const Challenge = ({
  actions,
  challenges,
  match: {
    params: { challengeId }
  }
}) => {
  const [challenge, setChallenge] = useState(null);
  useEffect(() => {
    if (!challenges.some(challenge => challenge.id === challengeId)) {
      actions.loadChallenge(challengeId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [challengeId]);
  useEffect(() => {
    setChallenge(challenges.find(challenge => challenge.id === challengeId));
  }, [challengeId, challenges]);
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

export default connectArenaChallenges(Challenge);
