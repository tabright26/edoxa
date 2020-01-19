import React, { FunctionComponent } from "react";
import { Card, CardHeader, Table } from "reactstrap";
import { ChallengeScoring, ChallengeId } from "types";
import { sentenceCase } from "change-case";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { compose } from "recompose";

interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteComponentProps<Params>;

interface StateProps {
  readonly scoring: ChallengeScoring;
}

interface Props {
  readonly challengeId: ChallengeId;
  readonly scoring: ChallengeScoring;
  readonly className?: string;
}

const Scoring: FunctionComponent<Props> = ({ scoring, className }) => (
  <Card className={className}>
    <CardHeader className="text-center bg-gray-900">
      <strong className="text-uppercase">Scoring legend</strong>
    </CardHeader>
    <Table striped bordered hover size="sm" className="m-0">
      <tbody>
        {Object.entries(scoring).map((item, index) => (
          <tr key={index}>
            <td className="pl-3 align-middle w-75">{sentenceCase(item[0])}</td>
            <td className="pr-3 align-middle w-25 text-right">{item[1]}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  </Card>
);

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge => challenge.id === ownProps.match.params.challengeId
  );
  return {
    scoring: challenge.scoring
  };
};

const enhance = compose<any, any>(withRouter, connect(mapStateToProps));

export default enhance(Scoring);
