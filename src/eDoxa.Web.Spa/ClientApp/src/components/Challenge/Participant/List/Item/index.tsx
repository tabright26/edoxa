import React, { FunctionComponent, useState } from "react";
import { Collapse, Card, Badge, CardHeader, CardBody } from "reactstrap";
import Format from "components/Shared/Format";
import { ChallengeParticipant } from "types";
import List from "components/Challenge/Participant/Match/List";
import { compose } from "recompose";
import {
  withUserProfileUserId,
  HocUserProfileUserIdStateProps
} from "utils/oidc/containers";

type Props = HocUserProfileUserIdStateProps & {
  participant: ChallengeParticipant;
  position: number;
  payoutEntries: number;
  bestOf: number;
};

const Item: FunctionComponent<Props> = ({
  participant,
  position,
  payoutEntries,
  bestOf,
  userId
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
          <div
            className={`px-3 py-2 ${participant.user.id === userId &&
              "text-primary"}`}
          >
            {collapse ? (
              <strong className="text-light">{doxatag}</strong>
            ) : (
              doxatag
            )}
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
          <CardHeader className="d-flex">
            <strong className="text-uppercase my-auto">Match history</strong>
            <small className="ml-2 my-auto text-muted">
              ({participant.matches.length}/{bestOf})
            </small>
          </CardHeader>
          <List participantId={participant.id} />
        </Card>
      </Collapse>
    </>
  );
};

const enhance = compose<any, any>(withUserProfileUserId);

export default enhance(Item);
