import React, { FunctionComponent } from "react";
import Item from "./Item";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { compose } from "recompose";
import { ChallengeId, ChallengeTimeline } from "types/challenges";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> & Params;

type StateProps = {
  readonly state: string;
  readonly timeline: ChallengeTimeline;
};

type InnerProps = OwnProps & StateProps;

type OutterProps = Params;

type Props = InnerProps & OutterProps;

const Timeline: FunctionComponent<Props> = ({ state, timeline }) => (
  <>
    <span className="text-uppercase btn btn-block text-light bg-gray-900">
      Timeline
    </span>
    <Item
      currentState={state}
      state="Inscription"
      unixTimeSeconds={timeline.createdAt}
    />
    <Item
      currentState={state}
      state="Started"
      unixTimeSeconds={timeline.startedAt}
    />
    <Item
      currentState={state}
      state="Ended"
      unixTimeSeconds={timeline.endedAt}
    />
    <Item
      currentState={state}
      state="Closed"
      unixTimeSeconds={timeline.closedAt}
    />
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
      (ownProps.challengeId
        ? ownProps.challengeId
        : ownProps.match.params.challengeId)
  );
  return {
    state: challenge.state,
    timeline: challenge.timeline
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(Timeline);
