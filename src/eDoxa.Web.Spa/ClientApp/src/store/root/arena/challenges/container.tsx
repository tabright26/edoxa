import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { loadChallenges, loadChallenge } from "store/root/arena/challenges/actions";
import { RootState } from "store/root/types";

export const connectArenaChallenges = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, challenges, ...attributes }) => <ConnectedComponent actions={actions} challenges={challenges} {...attributes} />;

  const mapStateToProps = (state: RootState) => {
    return {
      challenges: state.arena.challenges.data
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadChallenges: () => dispatch(loadChallenges()),
        loadChallenge: (challengeId: string) => dispatch(loadChallenge(challengeId))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

// DEPRECATED
// const mapStateToProps = (state: AppState, ownProps) => {
//   const challenge = state.arena.challenges.find(challenge => challenge.id === ownProps.match.params.challengeId);
//   if (challenge) {
//     challenge.participants.forEach(participant => {
//       participant.user = {
//         doxaTag: state.doxaTags.find(doxaTag => doxaTag.userId === participant.userId) || {
//           doxaTag: {
//             name: "[Unloaded]"
//           }
//         }
//       };
//     });
//   }
//   return {
//     challenge
//   };
// };
