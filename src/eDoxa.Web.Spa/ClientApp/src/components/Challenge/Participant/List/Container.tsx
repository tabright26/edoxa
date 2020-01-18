import { connect, MapStateToProps } from "react-redux";
import List from "./List";
import { RootState } from "store/types";
import { ChallengeId, ChallengeParticipant } from "types";
import { RouteComponentProps } from "react-router-dom";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";

interface Params {
  readonly challengeId: ChallengeId;
}

type OwnProps = RouteComponentProps<Params> & {
  readonly payoutEntries: number;
};

interface StateProps {
  readonly participants: ChallengeParticipant[];
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge => challenge.id === ownProps.match.params.challengeId
  );
  return {
    payoutEntries: challenge.payoutEntries,
    participants: challenge.participants
  };
};

const enhance = compose<any, any>(withRouter, connect(mapStateToProps));

export default enhance(List);
