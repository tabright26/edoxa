import React, { FunctionComponent, useEffect } from "react";
import { Row, Col, CardDeck, Card, CardBody } from "reactstrap";
import ChallengeLogo from "components/Challenge/Logo";
import ChallengeSummary from "components/Challenge/Summary";
import Scoreboard from "components/Challenge/Scoreboard";
import ChallengeScoring from "components/Challenge/Scoring";
import ChallengePayout from "components/Challenge/Payout";
import ChallengeTimeline from "components/Challenge/Timeline";
import Register from "components/Challenge/Register";
import { Loading } from "components/Shared/Loading";
import { Challenge, ChallengeId } from "types";
import GameCredentialModal from "components/Game/Credential/Modal";
import { compose } from "recompose";
import { connect, MapStateToProps, MapDispatchToProps } from "react-redux";
import { RootState } from "store/types";
import { loadChallenge } from "store/actions/challenge";
import { withRouter, RouteComponentProps } from "react-router-dom";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> & Params;

type StateProps = { challenge: Challenge };

type DispatchProps = { loadChallenge: () => void };

type InnerProps = StateProps & DispatchProps;

type OutterProps = Params;

type Props = InnerProps & OutterProps;

const Details: FunctionComponent<Props> = ({ challenge, loadChallenge }) => {
  useEffect((): void => {
    loadChallenge();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return !challenge ? (
    <Loading />
  ) : (
    <>
      <GameCredentialModal.Link />
      <GameCredentialModal.Unlink />
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
          <Register />
        </Col>
        <Col xs={{ size: 9, order: 3 }}>
          <Scoreboard />
        </Col>
      </Row>
    </>
  );
};

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data, loading } = state.root.challenge;
  return {
    challenge: data.find(
      challenge =>
        challenge.id ===
        (ownProps.match
          ? ownProps.match.params.challengeId
          : ownProps.challengeId)
    ),
    loading
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any,
  ownProps
) => {
  return {
    loadChallenge: () =>
      dispatch(
        loadChallenge(
          ownProps.match
            ? ownProps.match.params.challengeId
            : ownProps.challengeId
        )
      )
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(Details);
