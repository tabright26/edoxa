import React, { FunctionComponent } from "react";
import { Card, CardHeader, Table } from "reactstrap";
import { ChallengeScoring, ChallengeId } from "types";

interface Props {
  readonly challengeId: ChallengeId;
  readonly scoring: ChallengeScoring;
}

const Scoring: FunctionComponent<Props> = ({ scoring }) => (
  <Card className="my-2">
    <CardHeader className="text-center bg-gray-900">
      <strong className="text-uppercase">Scoring</strong>
    </CardHeader>
    <Table striped bordered hover size="sm" className="m-0">
      <tbody>
        {Object.entries(scoring).map((item, index) => (
          <tr key={index}>
            <td className="pl-3 align-middle">{item[0]}</td>
            <td className="pr-3 align-middle text-right">{item[1]}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  </Card>
);

export default Scoring;
