import { connect, MapStateToProps } from "react-redux";
import Payout from "./Payout";
import { RootState } from "store/types";
import { RouteChildrenProps } from "react-router";
import { ChallengeId, ChallengePayout } from "types";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";

interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteChildrenProps<Params>;

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
