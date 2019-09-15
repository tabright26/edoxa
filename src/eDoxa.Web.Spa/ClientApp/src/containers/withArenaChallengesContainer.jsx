import React, { Component } from "react";
import { connect } from "react-redux";
import { loadChallenges } from "../actions/arena/challenges/creators";

const withArenaChallengesContainer = WrappedComponent => {
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

export default withArenaChallengesContainer;
