import React, { FunctionComponent } from "react";
import { Row, Col, CardDeck, Card, Button, CardBody } from "reactstrap";
import ChallengeLogo from "components/Challenge/Logo/Logo";
import ChallengeSummary from "components/Challenge/Summary";
import ChallengeScoreboard from "components/Challenge/Scoreboard";
import ChallengeScoring from "components/Challenge/Scoring";
import ChallengePayout from "components/Challenge/Payout";
import ChallengeTimeline from "components/Challenge/Timeline";
import { withChallenge } from "store/root/arena/challenges/container";
import ErrorBoundary from "components/Shared/ErrorBoundary";
import Loading from "components/Shared/Loading";
import { Challenge } from "types";

interface Props {
  challenge: Challenge;
}

const ChallengeDetails: FunctionComponent<Props> = ({ challenge }) => {
  return (
    <ErrorBoundary>
      {!challenge ? (
        <Loading />
      ) : (
        <>
          <CardDeck className="my-4">
            <ChallengeLogo className="col-2 bg-gray-900" />
            <Card className="col-7">
              <CardBody className="d-flex">
                <ChallengeTimeline />
                <ChallengeSummary />
              </CardBody>
            </Card>

            <Card className="col-3 p-0">
              <Button className="h-100 bg-gray-900" size="lg">
                <strong className="text-uppercase" style={{ fontSize: "30px" }}>
                  REGISTER
                </strong>
              </Button>
            </Card>
          </CardDeck>
          <Row>
            <Col xs={10}>
              <ChallengeScoreboard />
            </Col>
            <Col xs={2}>
              <Row>
                <Col xs={12}>
                  <ChallengeScoring />
                </Col>
                <Col xs={12}>
                  <ChallengePayout />
                </Col>
              </Row>
            </Col>
          </Row>
        </>
      )}
    </ErrorBoundary>
  );
};

export default withChallenge(ChallengeDetails);
