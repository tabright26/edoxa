import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadChallenges } from "store/arena/challenges/actions";
import { AppState } from "store/types";

export const connectArenaChallenges = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, challenges, ...attributes }) => {
    useEffect((): void => {
      actions.loadChallenges();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
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
