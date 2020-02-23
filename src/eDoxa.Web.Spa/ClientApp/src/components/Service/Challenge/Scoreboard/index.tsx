import React, { FunctionComponent } from "react";
import { Card, CardHeader } from "reactstrap";
import List from "components/Service/Challenge/Participant/List";

const Scoreboard: FunctionComponent = () => (
  <>
    <Card className="text-center">
      <CardHeader>
        <strong className="text-uppercase">Scoreboard</strong>
      </CardHeader>
    </Card>
    <List />
  </>
);

export default Scoreboard;
