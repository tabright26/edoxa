import React, { FunctionComponent } from "react";
import Item from "./Item";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { RouteComponentProps } from "react-router-dom";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";
import produce, { Draft } from "immer";
import {
  ParticipantId,
  ChallengeId,
  ChallengeParticipantMatch
} from "types/challenges";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> &
  Params & {
    readonly participantId: ParticipantId;
  };

type StateProps = {
  readonly matches: ChallengeParticipantMatch[];
};

type InnerProps = OwnProps & StateProps;

type OutterProps = Params & { readonly participantId: ParticipantId };

type Props = InnerProps & OutterProps;

const List: FunctionComponent<Props> = ({ matches }) => (
  <>
    {matches.map((match, index) => (
      <Item key={index} match={match} position={index + 1} />
    ))}
  </>
);

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
  const participant = challenge.participants.find(
    participant => participant.id === ownProps.participantId
  );
  const matches = produce(
    participant.matches,
    (draft: Draft<ChallengeParticipantMatch[]>) => {
      draft
        .sort((left, right) => (left.score < right.score ? 1 : -1))
        .slice(0, challenge.bestOf);
    }
  );
  return {
    matches: produce(
      participant.matches,
      (draft: Draft<ChallengeParticipantMatch[]>) => {
        draft.forEach(match => {
          match.isBestOf = matches.some(s => s.id === match.id);
        });
      }
    )
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(List);
