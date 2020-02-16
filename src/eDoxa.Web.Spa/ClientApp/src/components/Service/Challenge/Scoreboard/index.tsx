import React, { FunctionComponent } from "react";
import { Card, CardHeader } from "reactstrap";
import List from "components/Service/Challenge/Participant/List";

const Scoreboard: FunctionComponent = () => (
  <>
    <Card className="my-4 text-center">
      <CardHeader className="bg-gray-900">
        <strong className="text-uppercase">Scoreboard</strong>
      </CardHeader>
    </Card>
    <List />
  </>
);

export default Scoreboard;
