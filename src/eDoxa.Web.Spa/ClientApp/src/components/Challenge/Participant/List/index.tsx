import React, { FunctionComponent } from "react";
import { connect, MapStateToProps } from "react-redux";
import Item from "./Item";
import { RootState } from "store/types";
import { ChallengeId, ChallengeParticipant } from "types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { compose } from "recompose";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> &
  Params & {
    readonly payoutEntries: number;
  };

type StateProps = {
  readonly participants: ChallengeParticipant[];
  readonly payoutEntries: number;
  readonly bestOf: number;
};

type InnerProps = OwnProps & StateProps;

type OutterProps = Params;

type Props = InnerProps & OutterProps;

const List: FunctionComponent<Props> = ({
  participants,
  payoutEntries,
  bestOf
}) => (
  <>
    {participants.map((participant, index) => (
      <Item
        key={index}
        participant={participant}
        position={index + 1}
        payoutEntries={payoutEntries}
        bestOf={bestOf}
      />
    ))}
  </>
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
    bestOf: challenge.bestOf,
    payoutEntries: challenge.payoutEntries,
    participants: challenge.participants
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(List);
