import React, { FunctionComponent } from "react";
import { CardTitle, Row, Col, Badge, Progress } from "reactstrap";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { ChallengeId, Game, ChallengeEntryFee, ChallengeState } from "types";
import { compose } from "recompose";
import Format from "components/Shared/Format";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> & Params;

type StateProps = {
  readonly name: string;
  readonly game: Game;
  readonly state: ChallengeState;
  readonly bestOf: number;
  readonly entries: number;
  readonly entryFee: ChallengeEntryFee;
  readonly payoutEntries: number;
  readonly participantCount: number;
};

type InnerProps = OwnProps & StateProps;

type OutterProps = Params;

type Props = InnerProps & OutterProps;

const Summary: FunctionComponent<Props> = ({
  name,
  game,
  state,
  bestOf,
  entries,
  entryFee,
  payoutEntries,
  participantCount
}) => (
  <Row>
    <Col>
      <CardTitle className="mt-2 pb-2 border-bottom border-primary">
        <strong className="text-uppercase">{name}</strong>
      </CardTitle>
      <Row>
        <Col>
          <dl className="row mb-0">
            <dd className="col-5 text-muted">Game</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                {game}
              </Badge>
            </dt>
            <dd className="col-5 text-muted">Entry fee</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                <Format.Currency
                  alignment="center"
                  currency={entryFee.currency}
                  amount={entryFee.amount}
                />
              </Badge>
            </dt>
            <dd className="col-5 text-muted">State</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                {state}
              </Badge>
            </dt>
          </dl>
        </Col>
        <Col>
          <dl className="row mb-0">
            <dd className="col-5 text-muted">Entries</dd>
            <dt className="col-7 text-right">
              <Progress
                color="dark"
                value={participantCount}
                max={entries}
                style={{ borderRadius: "10rem" }}
              >
                {`${participantCount}/${entries}`}
              </Progress>
            </dt>
            <dd className="col-5 text-muted">Payout entries</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                {payoutEntries}
              </Badge>
            </dt>
            <dd className="col-5 text-muted">Best of</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                {bestOf}
              </Badge>
            </dt>
          </dl>
        </Col>
      </Row>
    </Col>
  </Row>
);

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge =>
      challenge.id ===
      (ownProps.match
        ? ownProps.match.params.challengeId
        : ownProps.challengeId)
  );
  return {
    name: challenge.name,
    game: challenge.game,
    state: challenge.state,
    bestOf: challenge.bestOf,
    entries: challenge.entries,
    entryFee: challenge.payout.entryFee,
    payoutEntries: challenge.payoutEntries,
    participantCount: challenge.participants.length
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(Summary);
