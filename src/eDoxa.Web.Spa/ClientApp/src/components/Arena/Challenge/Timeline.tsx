import React from "react";
import { CardBody } from "reactstrap";

import Moment from "react-moment";
import Loading from "components/Shared/Override/Loading";

const style = { width: "200px" };

const ArenaChallengeTimeline = ({ challenge, state, date }) => {
  const isActiveState = challenge.state === state;
  return (
    <span className={`btn ${isActiveState ? "bg-info" : "bg-secondary"} text-light mt-2 rounded-0`} style={style} title={`${state}${isActiveState ? " (current)" : ""}`}>
      {date ? (
        <Moment unix format="lll">
          {date}
        </Moment>
      ) : (
        <span>--</span>
      )}
    </span>
  );
};

const Body = ({ challenge }) => {
  if (!challenge) {
    return (
      <CardBody className="text-center text-white">
        <Loading />
      </CardBody>
    );
  } else {
    return (
      <div
        className="d-flex flex-column position-relative"
        style={{
          right: "50px"
        }}
      >
        <span className="btn bg-primary text-light mt-2 rounded-0" style={style}>
          <strong>Timeline</strong>
        </span>
        <ArenaChallengeTimeline challenge={challenge} state="Inscription" date={challenge.timeline.createdAt} />
        <ArenaChallengeTimeline challenge={challenge} state="Started" date={challenge.timeline.startedAt} />
        <ArenaChallengeTimeline challenge={challenge} state="Ended" date={challenge.timeline.endedAt} />
        <ArenaChallengeTimeline challenge={challenge} state="Closed" date={challenge.timeline.closedAt} />
      </div>
    );
  }
};

const ChallengeTimeline = ({ challenge }) => <Body challenge={challenge} />;

export default ChallengeTimeline;
