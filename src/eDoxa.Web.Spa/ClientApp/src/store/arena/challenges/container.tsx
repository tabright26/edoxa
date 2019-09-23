import React, { FunctionComponent, useEffect } from "react";
import { Dispatch } from "redux";
import { connect } from "react-redux";
import { loadChallenges } from "store/arena/challenges/actions";
import { AppState } from "store/types";

const connectArenaChallenges = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, challenges, ...attributes }) => {
    useEffect((): void => {
      actions.loadChallenges();
    });
    return <ConnectedComponent actions={actions} challenges={challenges} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      challenges: state.arena.challenges
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
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
