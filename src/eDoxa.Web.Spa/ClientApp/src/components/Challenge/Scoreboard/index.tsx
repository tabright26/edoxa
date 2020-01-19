import React, { FunctionComponent } from "react";
import { Card, CardHeader } from "reactstrap";
import ChallengeParticipantList from "components/Challenge/Participant/List";

const ChallengeScoreboard: FunctionComponent = () => (
  <>
    <Card className="my-4 text-center">
      <CardHeader className="bg-gray-900">
        <strong className="text-uppercase">Scoreboard</strong>
      </CardHeader>
    </Card>
    <ChallengeParticipantList />
  </>
);

export default ChallengeScoreboard;
