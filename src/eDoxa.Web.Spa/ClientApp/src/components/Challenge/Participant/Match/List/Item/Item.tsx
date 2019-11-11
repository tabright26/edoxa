import React, { FunctionComponent } from "react";
import { CardBody, Badge } from "reactstrap";
import Format from "components/Shared/Format";
import { compose } from "recompose";
import { withModals } from "utils/modal/container";
import { ChallengeParticipantMatch } from "types";

interface Props {
  match: ChallengeParticipantMatch;
  position: number;
  modals: any;
}

const ChallengeParticipantMatchItem: FunctionComponent<Props> = ({ match, position, modals }) => (
  <CardBody className="p-0 border border-dark d-flex">
    <div
      className="pl-2 py-2 text-center"
      style={{
        width: "45px"
      }}
    >
      <Badge variant="light">{position}</Badge>
    </div>
    {/* <div
      className="px-3 py-2"
      style={{
        width: "350px"
      }}
    >
      <Moment unix format="LLLL">
        {match.synchronizedAt}
      </Moment>
    </div> */}
    <div className="py-2 text-center mx-auto" onClick={() => modals.showChallengeMatchScoreModal(match.stats)}>
      <Badge variant="primary">View details</Badge>
    </div>
    <div
      className="bg-primary px-3 py-2 text-center ml-5"
      style={{
        width: "125px"
      }}
    >
      <Format.Score score={match.score} />
    </div>
  </CardBody>
);

const enhance = compose<any, any>(withModals);

export default enhance(ChallengeParticipantMatchItem);
