import React, { Component } from "react";
import { connect } from "react-redux";
import { loadChallenge } from "actions/arena/challenges/creators";

export const withArenaChallengeContainer = WrappedComponent => {
  class ArenaChallengeContainer extends Component {
    componentDidMount() {
      this.props.actions.loadChallenge();
    }
    render() {
      const { challenge, ...rest } = this.props;
      return <WrappedComponent challenge={challenge} {...rest} />;
    }
  }

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
  )(ArenaChallengeContainer);
};

export default withArenaChallengeContainer;
