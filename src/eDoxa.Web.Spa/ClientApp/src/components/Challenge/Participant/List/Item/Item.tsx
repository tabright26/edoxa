import React, { FunctionComponent, useState } from "react";
import { Collapse, Card, Badge, CardHeader } from "reactstrap";
import Format from "components/Shared/Format";
import { ChallengeParticipant } from "types";
import ChallengeParticipantMatchList from "components/Challenge/Participant/Match/List";

interface Props {
  participant: ChallengeParticipant;
  position: number;
}

const ChallengeParticipantItem: FunctionComponent<Props> = ({ participant, position }) => {
  const [collapse, setCollapse] = useState(false);
  const toggle = () => setCollapse(!collapse);
  return (
    <>
      <Card className={`my-2 ${collapse ? "bg-primary" : ""}`} onClick={toggle}>
        <CardHeader className="p-0 d-flex">
          <div
            className="pl-2 py-2 text-center"
            style={{
              width: "45px"
            }}
          >
            <Badge variant="primary">{position}</Badge>
          </div>
          <div className="px-3 py-2">{participant.user ? participant.user.doxatag.name : "Data unavailable"}</div>
          <div
            className="bg-primary px-3 py-2 text-center ml-auto"
            style={{
              width: "125px"
            }}
          >
            <Format.Score score={participant.score} />
          </div>
        </CardHeader>
      </Card>
      <Collapse isOpen={collapse}>
        <Card>
          <CardHeader>Matches</CardHeader>
          <ChallengeParticipantMatchList participantId={participant.id} />
        </Card>
      </Collapse>
    </>
  );
};

export default ChallengeParticipantItem;
