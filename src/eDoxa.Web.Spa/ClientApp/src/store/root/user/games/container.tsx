import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadGames } from "store/root/user/games/actions";
import { RootState } from "store/root/types";

export const withUserGames = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, games, ...attributes }) => {
    useEffect((): void => {
      actions.loadGames();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent actions={actions} games={games} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      games: state.user.games.data
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadGames: () => dispatch(loadGames())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
