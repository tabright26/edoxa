import React, { FunctionComponent, useEffect } from "react";
import { Row, Col, Card, CardBody } from "reactstrap";
import Logo from "components/Service/Challenge/Logo";
import Summary from "components/Service/Challenge/Summary";
import Scoreboard from "components/Service/Challenge/Scoreboard";
import Scoring from "components/Service/Challenge/Scoring";
import Payout from "components/Service/Challenge/Payout";
import Timeline from "components/Service/Challenge/Timeline";
import Rules from "components/Service/Challenge/Rules";
import { Loading } from "components/Shared/Loading";
import GameCredentialModal from "components/Service/Game/Credential/Modal";
import { compose } from "recompose";
import { connect, MapStateToProps, MapDispatchToProps } from "react-redux";
import { RootState } from "store/types";
import { loadChallenge } from "store/actions/challenge";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { ChallengeId, Challenge } from "types/challenges";

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
        <Col xs={{ size: 8, order: 1 }}>
          <Card className="my-4">
            <CardBody>
              <Row>
                <Col md="3">
                  <Logo
                    className="h-100 bg-gray-900"
                    height={150}
                    width={150}
                  />
                </Col>
                <Col md="7">
                  <Summary challengeId={challenge.id} />
                </Col>
                <Col md="2">
                  <Timeline />
                </Col>
              </Row>
            </CardBody>
          </Card>
          <Scoreboard />
        </Col>
        <Col xs={{ size: 4, order: 2 }}>
          <Rules />
          <Payout />
          <Scoring className="mb-0" />
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
