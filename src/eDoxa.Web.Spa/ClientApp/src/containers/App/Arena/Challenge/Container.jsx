import React, { Component } from "react";
import { connect } from "react-redux";
import { loadChallenges, loadChallenge } from "../../../../store/actions/arenaChallengeActions";

export const withArenaChallengesContainer = WrappedComponent => {
  class ArenaChallengesContainer extends Component {
    componentDidMount() {
      this.props.actions.loadChallenges();
    }
    render() {
      const { challenges, ...rest } = this.props;
      return <WrappedComponent challenges={challenges} {...rest} />;
    }
  }

  const mapStateToProps = state => {
    return {
      challenges: state.arena.challenges
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadChallenges: () => dispatch(loadChallenges())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(ArenaChallengesContainer);
};

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
    challenge.participants.forEach(participant => {
      participant.user = {
        doxaTag: state.doxaTags.find(doxaTag => doxaTag.userId === participant.userId) || {
          doxaTag: {
            name: "[Unloaded]"
          }
        }
      };
    });
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
