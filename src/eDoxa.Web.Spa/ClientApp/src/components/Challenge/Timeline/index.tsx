import React, { FunctionComponent } from "react";
import ChallengeTimelineItem from "./Item";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { ChallengeId, ChallengeTimeline } from "types";
import { compose } from "recompose";

const style = { width: "150px" };

interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteComponentProps<Params>;

interface StateProps {
  readonly state: string;
  readonly timeline: ChallengeTimeline;
}

interface Props {
  readonly state: string;
  readonly timeline: ChallengeTimeline;
}

const Timeline: FunctionComponent<Props> = ({ state, timeline }) => (
  <div
    className="d-flex flex-column position-relative"
    style={{
      right: "50px"
    }}
  >
    <span className="btn bg-gray-900 mt-2 btn-sm rounded-0" style={style}>
      <strong className="text-uppercase">Timeline</strong>
    </span>
    <ChallengeTimelineItem
      currentState={state}
      state="Inscription"
      unixTimeSeconds={timeline.createdAt}
    />
    <ChallengeTimelineItem
      currentState={state}
      state="Started"
      unixTimeSeconds={timeline.startedAt}
    />
    <ChallengeTimelineItem
      currentState={state}
      state="Ended"
      unixTimeSeconds={timeline.endedAt}
    />
    <ChallengeTimelineItem
      currentState={state}
      state="Closed"
      unixTimeSeconds={timeline.closedAt}
    />
  </div>
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
    state: challenge.state,
    timeline: challenge.timeline
  };
};

const enhance = compose<any, any>(withRouter, connect(mapStateToProps));

export default enhance(Timeline);
