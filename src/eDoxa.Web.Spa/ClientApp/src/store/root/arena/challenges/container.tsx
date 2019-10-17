import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadChallenges, loadChallenge } from "store/root/arena/challenges/actions";
import { RootState } from "store/root/types";

export const withChallenges = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadChallenges();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      challenges: state.arena.challenges
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadChallenges: () => dispatch(loadChallenges())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export const withChallenge = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadChallenge();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState, ownProps: any) => {
    const { data } = state.arena.challenges;
    return {
      challenge: data.find(challenge => challenge.id === ownProps.match.params.challengeId)
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: any) => {
    return {
      loadChallenge: () => dispatch(loadChallenge(ownProps.match.params.challengeId))
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
