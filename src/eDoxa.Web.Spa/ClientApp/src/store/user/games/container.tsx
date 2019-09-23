import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadGames } from "store/user/games/actions";
import { AppState } from "store/types";

const connectUserGames = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, games, ...attributes }) => {
    useEffect((): void => {
      actions.loadGames();
    });
    return <ConnectedComponent actions={actions} games={games} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      games: state.user.games
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

export default connectUserGames;
