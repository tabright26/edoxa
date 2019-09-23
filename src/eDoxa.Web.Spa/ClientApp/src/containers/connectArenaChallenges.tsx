import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadChallenges } from "reducers/arena/challenges/actions";

const connectArenaChallenges = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, challenges, ...attributes }) => {
    useEffect((): void => {
      actions.loadChallenges();
    });
    return <ConnectedComponent actions={actions} challenges={challenges} {...attributes} />;
  };

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
