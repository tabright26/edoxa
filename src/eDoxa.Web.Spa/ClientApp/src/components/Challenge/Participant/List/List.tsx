import React, { FunctionComponent } from "react";

import ChallengeParticipantItem from "./Item";
import { ChallengeParticipant } from "types";

interface Props {
  participants: ChallengeParticipant[];
  payoutEntries: number;
  bestOf: number;
}

const ChallengeParticipantList: FunctionComponent<Props> = ({
  participants,
  payoutEntries,
  bestOf
}) => (
  <>
    {participants.map((participant, index) => (
      <ChallengeParticipantItem
        key={index}
        participant={participant}
        position={index + 1}
        payoutEntries={payoutEntries}
        bestOf={bestOf}
      />
    ))}
  </>
);

export default ChallengeParticipantList;
