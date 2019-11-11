import { connect, MapStateToProps } from "react-redux";
import Summary from "./Summary";
import { RootState } from "store/types";
import { RouteChildrenProps } from "react-router";
import { ChallengeId, Game, ChallengeEntryFee } from "types";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";

interface Params {
  readonly challengeId: ChallengeId;
}

interface OwnProps extends RouteChildrenProps<Params> {
  readonly challengeId?: ChallengeId;
}

interface StateProps {
  readonly name: string;
  readonly game: Game;
  readonly bestOf: number;
  readonly entries: number;
  readonly entryFee: ChallengeEntryFee;
  readonly payoutEntries: number;
  readonly participantCount: number;
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state, ownProps) => {
  const { data } = state.root.arena.challenges;
  const challenge = data.find(challenge => challenge.id === (ownProps.match ? ownProps.match.params.challengeId : ownProps.challengeId));
  return {
    name: challenge.name,
    game: challenge.game,
    bestOf: challenge.bestOf,
    entries: challenge.entries,
    entryFee: challenge.entryFee,
    payoutEntries: challenge.payoutEntries,
    participantCount: challenge.participants.length
  };
};

const enhance = compose<any, any>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(Summary);
