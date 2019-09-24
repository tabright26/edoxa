import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadChallenge } from "store/arena/challenges/actions";
import { AppState } from "store/types";

export const connectArenaChallenge = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, challenge, ...attributes }) => {
    useEffect((): void => {
      actions.loadChallenge();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} challenge={challenge} {...attributes} />;
  };

  const mapStateToProps = (state: AppState, ownProps) => {
    const challenge = state.arena.challenges.find(challenge => challenge.id === ownProps.match.params.challengeId);
    if (challenge) {
      challenge.participants.forEach(participant => {
        participant.user = {
          doxaTag: state.doxaTags.find(doxaTag => doxaTag.userId === participant.userId) || {
            doxaTag: {
              name: "[Unloaded]"
            }
          }
        };
      });
    }
    return {
      challenge
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps) => {
    return {
      actions: {
        loadChallenge: () => dispatch(loadChallenge(ownProps.match.params.challengeId))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
