import React from "react";
import { Card, CardHeader } from "reactstrap";

const ArenaChallengeScoreboard = ({ challenge }) => (
  <>
    <Card bg="dark" className="my-2 text-light text-center">
      <CardHeader as="h5" className="border-0">
        Scoreboard
      </CardHeader>
    </Card>
    {/* <Scrollbar
      style={{
        height: "500px"
      }}
    >
      <Collapse>
        <ArenaChallengeParticipantIndex challenge={challenge} />
      </Collapse>
    </Scrollbar> */}
  </>
);

export default ArenaChallengeScoreboard;
