import React from "react";
import { Card } from "react-bootstrap";

import Loading from "../../../../containers/Shared/Loading";

import ArenaChallengeParticipantDetails from "./Details";

const ArenaChallengeParticipantIndex = ({ challenge }) => {
  if (!challenge) {
    return (
      <Card.Body className="text-center mt-5">
        <Loading />
      </Card.Body>
    );
  } else {
    return (
      <>
        {challenge.participants
          .sort((left, right) => (left.averageScore < right.averageScore ? 1 : -1))
          .map((participant, index) => (
            <ArenaChallengeParticipantDetails key={index} participant={participant} position={index + 1} />
          ))}
      </>
    );
  }
};

export default ArenaChallengeParticipantIndex;
