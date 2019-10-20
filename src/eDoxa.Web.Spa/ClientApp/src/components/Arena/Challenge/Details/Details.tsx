import React from "react";
import { Row, Col } from "reactstrap";

import ChallengeLogo from "../Logo/Logo";
import ChallengeSetup from "components/Arena/Challenge/Summary/Summary";
import ChallengeScoreboard from "../Scoreboard/Scoreboard";
import ChallengeScoring from "../Scoring/Scoring";
import ChallengePayout from "../Payout/Payout";

import { withChallenge } from "store/root/arena/challenges/container";

import ErrorBoundary from "components/Shared/ErrorBoundary";

const Challenge = ({ challenge }) => {
  return (
    <ErrorBoundary>
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
    </ErrorBoundary>
  );
};

export default withChallenge(Challenge);
