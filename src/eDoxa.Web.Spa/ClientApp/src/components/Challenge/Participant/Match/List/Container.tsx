import { connect, MapStateToProps } from "react-redux";
import List from "./List";
import { RootState } from "store/types";
import { ChallengeId, ParticipantId, ChallengeParticipantMatch } from "types";
import { RouteComponentProps } from "react-router-dom";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";

interface Params {
  readonly challengeId: ChallengeId;
}

interface OwnProps extends RouteComponentProps<Params> {
  readonly participantId: ParticipantId;
}

interface StateProps {
  readonly matches: ChallengeParticipantMatch[];
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state, ownProps) => {
  const { data } = state.root.challenge;
  const challenge = data.find(challenge => challenge.id === ownProps.match.params.challengeId);
  const participant = challenge.participants.find(participant => participant.id === ownProps.participantId);
  return {
    matches: participant.matches
  };
};

const enhance = compose<any, any>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(List);