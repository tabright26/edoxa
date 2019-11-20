import { connect, MapStateToProps } from "react-redux";
import Payout from "./Payout";
import { RootState } from "store/types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { ChallengeId, ChallengePayout } from "types";
import { compose } from "recompose";

interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteComponentProps<Params>;

interface StateProps {
  readonly payout: ChallengePayout;
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state, ownProps) => {
  const { data } = state.root.challenge;
  const challenge = data.find(challenge => challenge.id === ownProps.match.params.challengeId);
  return {
    payout: challenge.payout
  };
};

const enhance = compose<any, any>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(Payout);
