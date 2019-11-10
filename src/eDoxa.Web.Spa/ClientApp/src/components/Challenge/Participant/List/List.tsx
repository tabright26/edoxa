import React, { FunctionComponent } from "react";

import ChallengeParticipantItem from "./Item";
import { ChallengeParticipant } from "types";

interface Props {
  participants: ChallengeParticipant[];
}

const ChallengeParticipantList: FunctionComponent<Props> = ({ participants }) => (
  <>
    {participants.map((participant, index) => (
      <ChallengeParticipantItem key={index} participant={participant} position={index + 1} />
    ))}
  </>
);

export default ChallengeParticipantList;
