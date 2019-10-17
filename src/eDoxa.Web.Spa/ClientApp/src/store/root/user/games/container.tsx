import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserGames } from "store/root/user/games/actions";
import { RootState } from "store/root/types";

export const withUserGames = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserGames();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      games: state.user.games
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadUserGames: () => dispatch(loadUserGames())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
