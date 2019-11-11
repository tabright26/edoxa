import { connect, MapStateToProps } from "react-redux";
import List from "./List";
import { RootState } from "store/types";
import { ChallengeId, ChallengeParticipant } from "types";
import { RouteChildrenProps } from "react-router";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";

interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteChildrenProps<Params>;

interface StateProps {
  readonly participants: ChallengeParticipant[];
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state, ownProps) => {
  const { data } = state.root.arena.challenges;
  const challenge = data.find(challenge => challenge.id === ownProps.match.params.challengeId);
  return {
    participants: challenge.participants
  };
};

const enhance = compose<any, any>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(List);
