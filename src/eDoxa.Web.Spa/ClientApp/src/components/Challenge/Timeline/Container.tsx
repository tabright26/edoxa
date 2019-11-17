import { connect, MapStateToProps } from "react-redux";
import Timeline from "./Timeline";
import { RootState } from "store/types";
import { RouteChildrenProps } from "react-router";
import { ChallengeId, ChallengeTimeline } from "types";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";

interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteChildrenProps<Params>;

interface StateProps {
  readonly state: string;
  readonly timeline: ChallengeTimeline;
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state, ownProps) => {
  const { data } = state.root.challenge;
  const challenge = data.find(challenge => challenge.id === ownProps.match.params.challengeId);
  return {
    state: challenge.state,
    timeline: challenge.timeline
  };
};

const enhance = compose<any, any>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(Timeline);
