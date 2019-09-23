import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadGames } from "reducers/user/games/actions";

const connectUserGames = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, games, ...attributes }) => {
    useEffect((): void => {
      actions.loadGames();
    });
    return <ConnectedComponent actions={actions} games={games} {...attributes} />;
  };

  const mapStateToProps = state => {
    return {
      games: state.user.games
    };
  };

  const mapDispatchToProps = dispatch => {
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
