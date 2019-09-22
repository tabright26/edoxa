import React, { Component } from "react";
import { connect } from "react-redux";
import { loadChallenges } from "actions/arena/challenges/creators";

const connectArenaChallenges = WrappedComponent => {
  class Container extends Component<any> {
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
  )(Container);
};

export default connectArenaChallenges;
