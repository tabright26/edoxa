import React from "react";
import { CardBody } from "reactstrap";

import Loading from "components/Shared/Loading";

import ArenaChallengeParticipantDetails from "components/Arena/Challenge/Participant/Details/Details";

const ArenaChallengeParticipantIndex = ({ challenge }) => {
  if (!challenge) {
    return (
      <CardBody className="text-center mt-5">
        <Loading />
      </CardBody>
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
