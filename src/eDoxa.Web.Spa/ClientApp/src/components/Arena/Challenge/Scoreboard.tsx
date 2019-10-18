import React from "react";
import { Card, CardHeader, Collapse } from "reactstrap";

import Scrollbar from "react-scrollbars-custom";

import ArenaChallengeParticipantIndex from "./Participant/Index";

const ArenaChallengeScoreboard = ({ challenge }) => (
  <>
    <Card bg="dark" className="my-2 text-light text-center">
      <CardHeader as="h5" className="border-0">
        Scoreboard
      </CardHeader>
    </Card>
    <Scrollbar
      style={{
        height: "500px"
      }}
    >
      <Collapse>
        <ArenaChallengeParticipantIndex challenge={challenge} />
      </Collapse>
    </Scrollbar>
  </>
);

export default ArenaChallengeScoreboard;
