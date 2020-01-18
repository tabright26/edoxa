import React, { FunctionComponent, useState } from "react";
import { Collapse, Card, Badge, CardHeader, CardBody } from "reactstrap";
import Format from "components/Shared/Format";
import { ChallengeParticipant } from "types";
import ChallengeParticipantMatchList from "components/Challenge/Participant/Match/List";

interface Props {
  participant: ChallengeParticipant;
  position: number;
  payoutEntries: number;
}

const ChallengeParticipantItem: FunctionComponent<Props> = ({
  participant,
  position,
  payoutEntries
}) => {
  const [collapse, setCollapse] = useState(false);
  const toggle = () => setCollapse(!collapse);
  const doxatag = participant.user.doxatag
    ? participant.user.doxatag.name
    : "Data unavailable";
  return (
    <>
      <Card className="my-2" onClick={toggle}>
        <CardBody
          className={`p-0 d-flex ${
            collapse ? "bg-primary" : ""
          } border-0 rounded`}
        >
          <div
            className="pl-3 py-2 text-center"
            style={{
              width: "50px"
            }}
          >
            <Badge
              className={`${
                collapse
                  ? "bg-gray-700"
                  : position <= payoutEntries && participant.score
                  ? "bg-primary"
                  : "bg-secondary"
              } w-100`}
            >
              {collapse ? <strong>{position}</strong> : position}
            </Badge>
          </div>
          <div className="px-3 py-2">
            {collapse ? <strong>{doxatag}</strong> : doxatag}
          </div>
          <div
            className={`${
              collapse ? "bg-gray-700" : "bg-primary"
            } px-3 py-2 text-center ml-auto h-100 rounded-right`}
            style={{
              width: "125px"
            }}
          >
            <Format.Score score={participant.score} bold={collapse} />
          </div>
        </CardBody>
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
