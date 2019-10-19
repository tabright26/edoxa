import React from "react";
import { CardBody } from "reactstrap";

//import Format from "Shared/Format";
import Loading from "components/Shared/Override/Loading";

//import Matches from "./Match/Index";

const ArenaChallengeParticipantDetails = ({ participant, position }) => {
  if (!participant) {
    return (
      <CardBody className="text-center mt-5">
        <Loading />
      </CardBody>
    );
  } else {
    return (
      <>
        {/* <Collapse as="div" eventKey={position - 1} className="participant">
          <Card bg="dark" className="my-2 text-light">
            <CardBody className="p-0 d-flex">
              <div
                className="pl-2 py-2 text-center"
                style={{
                  width: "45px"
                }}
              >
                <Badge variant="primary">{position}</Badge>
              </div>
              <div className="px-3 py-2">{participant.user.doxatag.name || "Unavailable"}</div>
              <div
                className="bg-primary px-3 py-2 text-center ml-auto"
                style={{
                  width: "90px"
                }}
              >
                <Format.Score score={participant.averageScore} />
              </div>
            </CardBody>
          </Card>
        </Collapse>
        <Collapse eventKey={position - 1}>
          <Card bg="dark" className="text-light">
            <CardHeader as="h5">Matches</CardHeader>
            <Matches participant={participant} />
          </Card>
        </Collapse> */}
      </>
    );
  }
};

export default ArenaChallengeParticipantDetails;
