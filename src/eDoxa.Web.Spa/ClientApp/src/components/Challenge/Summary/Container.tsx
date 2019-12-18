import { connect, MapStateToProps } from "react-redux";
import Summary from "./Summary";
import { RootState } from "store/types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { ChallengeId, Game, ChallengeEntryFee, ChallengeState } from "types";
import { compose } from "recompose";

interface Params {
  readonly challengeId: ChallengeId;
}

interface OwnProps extends RouteComponentProps<Params> {
  readonly challengeId?: ChallengeId;
}

interface StateProps {
  readonly name: string;
  readonly game: Game;
  readonly state: ChallengeState;
  readonly bestOf: number;
  readonly entries: number;
  readonly entryFee: ChallengeEntryFee;
  readonly payoutEntries: number;
  readonly participantCount: number;
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge =>
      challenge.id ===
      (ownProps.match
        ? ownProps.match.params.challengeId
        : ownProps.challengeId)
  );
  return {
    name: challenge.name,
    game: challenge.game,
    state: challenge.state,
    bestOf: challenge.bestOf,
    entries: challenge.entries,
    entryFee: challenge.payout.entryFee,
    payoutEntries: challenge.payoutEntries,
    participantCount: challenge.participants.length
  };
};

const enhance = compose<any, any>(withRouter, connect(mapStateToProps));

export default enhance(Summary);
