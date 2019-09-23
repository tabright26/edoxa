import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadChallenge } from "reducers/arena/challenges/actions";

export const connectArenaChallenge = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, challenge, ...attributes }) => {
    useEffect((): void => {
      actions.loadChallenge();
    });
    return <ConnectedComponent actions={actions} challenge={challenge} {...attributes} />;
  };

  const mapStateToProps = (state, ownProps) => {
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

  const mapDispatchToProps = (dispatch, ownProps) => {
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

export default connectArenaChallenge;
