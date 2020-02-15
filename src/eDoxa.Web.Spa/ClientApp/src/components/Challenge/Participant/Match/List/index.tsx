import React, { FunctionComponent } from "react";
import Item from "./Item";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { RouteComponentProps } from "react-router-dom";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";
import produce, { Draft } from "immer";
import {
  ChallengeParticipantId,
  ChallengeId,
  ChallengeMatch
} from "types/challenges";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> &
  Params & {
    readonly participantId: ChallengeParticipantId;
  };

type StateProps = {
  readonly matches: ChallengeMatch[];
};

type InnerProps = OwnProps & StateProps;

type OutterProps = Params & { readonly participantId: ChallengeParticipantId };

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
    (draft: Draft<ChallengeMatch[]>) => {
      draft
        .sort((left, right) => (left.score < right.score ? 1 : -1))
        .slice(0, challenge.bestOf);
    }
  );
  return {
    matches: produce(
      participant.matches,
      (draft: Draft<ChallengeMatch[]>) => {
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
