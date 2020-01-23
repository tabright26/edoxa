import React, { FunctionComponent } from "react";
import ChallengeParticipantMatchItem from "./Item";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { ChallengeId, ParticipantId, ChallengeParticipantMatch } from "types";
import { RouteComponentProps } from "react-router-dom";
import { compose } from "recompose";
import { withRouter } from "react-router-dom";
import produce, { Draft } from "immer";

interface Props {
  participantId: ParticipantId;
  matches: ChallengeParticipantMatch[];
}

interface Params {
  readonly challengeId: ChallengeId;
}

interface OwnProps extends RouteComponentProps<Params> {
  readonly participantId: ParticipantId;
}

interface StateProps {
  readonly matches: ChallengeParticipantMatch[];
}

const ChallengeParticipantMatchList: FunctionComponent<Props> = ({
  matches
}) => (
  <>
    {matches.map((match, index) => (
      <ChallengeParticipantMatchItem
        key={index}
        match={match}
        position={index + 1}
      />
    ))}
  </>
);

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge => challenge.id === ownProps.match.params.challengeId
  );
  const participant = challenge.participants.find(
    participant => participant.id === ownProps.participantId
  );
  let matches = produce(
    participant.matches,
    (draft: Draft<ChallengeParticipantMatch[]>) => {
      draft.sort((left, right) => (left.score < right.score ? 1 : -1));
    }
  );
  matches = matches.slice(0, challenge.bestOf);
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

const enhance = compose<any, any>(withRouter, connect(mapStateToProps));

export default enhance(ChallengeParticipantMatchList);
