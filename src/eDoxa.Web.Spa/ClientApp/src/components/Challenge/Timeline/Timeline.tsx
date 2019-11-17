import React, { FunctionComponent } from "react";
import ChallengeTimelineItem from "./Item";
import { ChallengeTimeline } from "types";

const style = { width: "150px" };

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
    <ChallengeTimelineItem currentState={state} state="Inscription" unixTimeSeconds={timeline.createdAt} />
    <ChallengeTimelineItem currentState={state} state="Started" unixTimeSeconds={timeline.startedAt} />
    <ChallengeTimelineItem currentState={state} state="Ended" unixTimeSeconds={timeline.endedAt} />
    <ChallengeTimelineItem currentState={state} state="Closed" unixTimeSeconds={timeline.closedAt} />
  </div>
);

export default Timeline;
