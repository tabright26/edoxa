import React, { FunctionComponent } from "react";
import Moment from "react-moment";

const style = { width: "150px" };

interface Props {
  readonly currentState: string;
  readonly state: string;
  readonly unixTimeSeconds: number;
}

const ChallengeTimelineItem: FunctionComponent<Props> = ({ currentState, state, unixTimeSeconds }) => {
  const isActiveState = currentState.toLowerCase() === state.toLowerCase();
  return (
    <span className={`btn ${isActiveState ? "bg-primary" : "bg-secondary"} btn-sm text-light mt-2 rounded-0`} style={style} title={`${state}${isActiveState ? " (current)" : ""}`}>
      {unixTimeSeconds ? (
        <Moment unix format="lll">
          {unixTimeSeconds}
        </Moment>
      ) : (
        <span>...</span>
      )}
    </span>
  );
};

export default ChallengeTimelineItem;
