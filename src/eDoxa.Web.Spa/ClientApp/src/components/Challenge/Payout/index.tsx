import React, { FunctionComponent } from "react";
import { Table, Card, CardHeader } from "reactstrap";
import Format from "components/Shared/Format";
import { ChallengeId, ChallengePayout } from "types";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { compose } from "recompose";

interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteComponentProps<Params>;

interface StateProps {
  readonly payout: ChallengePayout;
}

interface Props {
  readonly challengeId: ChallengeId;
  readonly payout: ChallengePayout;
}

const ArenaChallengePayout: FunctionComponent<Props> = ({ payout }) => {
  let prevSize = 1;
  let nextSize = 0;
  return (
    <Card className="text-center">
      <CardHeader className="bg-gray-900">
        <strong className="text-uppercase">Payout</strong>
      </CardHeader>
      <Table striped bordered hover size="sm" variant="dark" className="m-0">
        <thead>
          <tr>
            <th className="align-middle w-50">Position</th>
            <th className="align-middle w-50">Prize</th>
          </tr>
        </thead>
        <tbody>
          {payout.buckets.map((bucket, index) => {
            const bucketSize = bucket.size;
            nextSize += bucketSize;
            const bucketRender = (
              <tr key={index}>
                <td className="align-middle w-50">
                  <Format.PayoutBucket
                    prevSize={prevSize}
                    nextSize={nextSize}
                  />
                </td>
                <td className="align-middle w-50">
                  <Format.Currency
                    alignment="center"
                    currency={payout.prizePool.currency}
                    amount={bucket.prize}
                  />
                </td>
              </tr>
            );
            prevSize += bucketSize;
            return bucketRender;
          })}
        </tbody>
      </Table>
    </Card>
  );
};

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge => challenge.id === ownProps.match.params.challengeId
  );
  return {
    payout: challenge.payout
  };
};

const enhance = compose<any, any>(withRouter, connect(mapStateToProps));

export default enhance(ArenaChallengePayout);
