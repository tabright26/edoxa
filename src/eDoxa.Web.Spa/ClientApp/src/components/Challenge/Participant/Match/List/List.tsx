import React, { FunctionComponent } from "react";
import ChallengeParticipantMatchItem from "./Item";
import { ChallengeParticipantMatch, ParticipantId } from "types";

interface Props {
  participantId: ParticipantId;
  matches: ChallengeParticipantMatch[];
}

const ChallengeParticipantMatchList: FunctionComponent<Props> = ({ matches }) => (
  <>
    {matches.map((match, index) => (
      <ChallengeParticipantMatchItem key={index} match={match} position={index + 1} />
    ))}
  </>
);

export default ChallengeParticipantMatchList;
