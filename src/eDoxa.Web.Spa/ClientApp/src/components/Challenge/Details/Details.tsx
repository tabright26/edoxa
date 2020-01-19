import React, { FunctionComponent } from "react";
import { Row, Col, CardDeck, Card, CardBody } from "reactstrap";
import ChallengeLogo from "components/Challenge/Logo/Logo";
import ChallengeSummary from "components/Challenge/Summary";
import ChallengeScoreboard from "components/Challenge/Scoreboard";
import ChallengeScoring from "components/Challenge/Scoring";
import ChallengePayout from "components/Challenge/Payout";
import ChallengeTimeline from "components/Challenge/Timeline";
import ChallengeRegister from "components/Challenge/Register";
import { withChallenge } from "store/root/challenge/container";
import { Loading } from "components/Shared/Loading";
import { Challenge } from "types";

interface Props {
  challenge: Challenge;
}

const ChallengeDetails: FunctionComponent<Props> = ({ challenge }) =>
  !challenge ? (
    <Loading />
  ) : (
    <Row>
      <Col xs={{ size: 9, order: 1 }}>
        <CardDeck className="mt-4">
          <ChallengeLogo className="col-2 bg-gray-900" />
          <Card className="col-10">
            <CardBody className="d-flex">
              <ChallengeTimeline />
              <ChallengeSummary />
            </CardBody>
          </Card>
        </CardDeck>
      </Col>
      <Col xs={{ size: 3, order: 2 }}>
        <ChallengeScoring className="mt-4" />
      </Col>
      <Col xs={{ size: 3, order: 4 }}>
        <ChallengePayout />
        <ChallengeRegister />
      </Col>
      <Col xs={{ size: 9, order: 3 }}>
        <ChallengeScoreboard />
      </Col>
    </Row>
  );

export default withChallenge(ChallengeDetails);
