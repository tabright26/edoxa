import React, { FunctionComponent } from "react";
import { connect, MapStateToProps } from "react-redux";
import ChallengeParticipantItem from "./Item";
import { RootState } from "store/types";
import { ChallengeId, ChallengeParticipant } from "types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { compose } from "recompose";

interface Props {
  participants: ChallengeParticipant[];
  payoutEntries: number;
  bestOf: number;
}
interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteComponentProps<Params> & {
  readonly payoutEntries: number;
};

interface StateProps {
  readonly participants: ChallengeParticipant[];
}

const ChallengeParticipantList: FunctionComponent<Props> = ({
  participants,
  payoutEntries,
  bestOf
}) => (
  <>
    {participants.map((participant, index) => (
      <ChallengeParticipantItem
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
    challenge => challenge.id === ownProps.match.params.challengeId
  );
  return {
    bestOf: challenge.bestOf,
    payoutEntries: challenge.payoutEntries,
    participants: challenge.participants
  };
};

const enhance = compose<any, any>(withRouter, connect(mapStateToProps));

export default enhance(ChallengeParticipantList);
